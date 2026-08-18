# a2z Tech Inventory Management System

Desktop inventory app for **a2z Tech** (sky-blue theme). Same product as Generic / Otargi / Panache — only branding and local port differ.

## Quick Start

### Run from source

```powershell
dotnet run -c Release --project A2ZTech.csproj
```

Or:

```text
bin\Release\net8.0-windows\win-x64\A2ZTech.exe
```

### Publish

```powershell
.\publish.bat
```

- App folder: `dist\app\A2ZTech.exe`
- Portable: `dist\A2ZTech-Portable.exe`
- Setup: `dist\A2ZTechSetup.exe` (if Inno Setup is installed)

### Prerequisites

- Windows 10/11 64-bit and WebView2 Runtime
- .NET 8 SDK only to build from source

### First-Time Setup

1. **License Activation** (if prompted)
2. **Default Login** — `Softio.Admin` / `Softio@2026!`
3. **Database** — `%LocalAppData%\A2ZTech\Data\inventory.db`

Local HTTP port: **5030** (so it can run next to Generic 5000, Otargi 5010, Panache 5020).
