using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace InventorySystem
{
    /// <summary>
    /// Centralized database and file path configuration (SQLite).
    /// Writable data lives under LocalAppData so Program Files installs work without elevation.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>Root for user-writable app data (%LocalAppData%\A2ZTech).</summary>
        public static string UserDataRoot
        {
            get
            {
                string root = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "A2ZTech");
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
                return root;
            }
        }

        public static string ConnectionString
        {
            get
            {
                string dbPath = DatabasePath;
                string dir = Path.GetDirectoryName(dbPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return $"Data Source={dbPath};";
            }
        }

        /// <summary>
        /// SQLite DB under LocalAppData. Migrates once from install-folder Data if needed.
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                string dir = Path.Combine(UserDataRoot, "Data");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string path = Path.Combine(dir, "inventory.db");

                try
                {
                    string legacy = Path.Combine(Application.StartupPath ?? "", "Data", "inventory.db");
                    if (!File.Exists(path) && File.Exists(legacy))
                        File.Copy(legacy, path, overwrite: false);
                }
                catch { /* migration is best-effort */ }

                return path;
            }
        }

        /// <summary>Product images for the web UI (served at /Assets/Products/...).</summary>
        public static string ProductsImagesDirectory
        {
            get
            {
                string imagesPath = Path.Combine(UserDataRoot, "Assets", "Products");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                try
                {
                    string legacy = Path.Combine(Application.StartupPath ?? "", "Assets", "Products");
                    if (Directory.Exists(legacy))
                    {
                        foreach (string file in Directory.GetFiles(legacy))
                        {
                            string dest = Path.Combine(imagesPath, Path.GetFileName(file));
                            if (!File.Exists(dest))
                                File.Copy(file, dest, overwrite: false);
                        }
                    }
                }
                catch { /* migration best-effort */ }

                return imagesPath;
            }
        }

        /// <summary>Legacy WinForms parts images folder (also under LocalAppData).</summary>
        public static string PartsImagesDirectory
        {
            get
            {
                string imagesPath = Path.Combine(UserDataRoot, "Parts_Images");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);
                return imagesPath;
            }
        }

        /// <summary>Safely replace the live DB file from a backup/import source.</summary>
        public static void ReplaceDatabaseFrom(string sourceDbPath)
        {
            if (string.IsNullOrWhiteSpace(sourceDbPath) || !File.Exists(sourceDbPath))
                throw new FileNotFoundException("Source database not found.", sourceDbPath);

            string dbFile = DatabasePath;
            string dir = Path.GetDirectoryName(dbFile);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            SqliteConnection.ClearAllPools();

            foreach (string side in new[] { dbFile + "-wal", dbFile + "-shm" })
            {
                try { if (File.Exists(side)) File.Delete(side); } catch { /* ignore */ }
            }

            string temp = dbFile + ".incoming";
            File.Copy(sourceDbPath, temp, overwrite: true);

            try
            {
                File.Copy(temp, dbFile, overwrite: true);
            }
            catch
            {
                try { if (File.Exists(dbFile)) File.Delete(dbFile); } catch { /* may still be locked */ }
                File.Copy(temp, dbFile, overwrite: true);
            }

            try { File.Delete(temp); } catch { /* ignore */ }

            foreach (string side in new[] { dbFile + "-wal", dbFile + "-shm" })
            {
                try { if (File.Exists(side)) File.Delete(side); } catch { /* ignore */ }
            }

            SqliteConnection.ClearAllPools();
        }
    }
}
