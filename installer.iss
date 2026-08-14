AppName=a2z Tech
AppVersion=1.0
DefaultDirName={autopf}\a2z Tech
DefaultGroupName=a2z Tech
OutputDir=InstallerOutput
OutputBaseFilename=A2ZTechSetup
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64compatible
SetupIconFile=Assets\icon.ico
UninstallDisplayIcon={app}\A2ZTech.exe

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop shortcut"; GroupDescription: "Additional icons:"; Flags: checkedonce

[Files]
Source: "publish-output\A2ZTech.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish-output\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: "A2ZTech.exe,appsettings.json,Assets,wwwroot,Plugins"
Source: "appsettings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "Assets\*"; DestDir: "{app}\Assets"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "wwwroot\*"; DestDir: "{app}\wwwroot"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\a2z Tech"; Filename: "{app}\A2ZTech.exe"; IconFilename: "{app}\Assets\icon.ico"
Name: "{autodesktop}\a2z Tech"; Filename: "{app}\A2ZTech.exe"; IconFilename: "{app}\Assets\icon.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\A2ZTech.exe"; Description: "{cm:LaunchProgram,a2z Tech}"; Flags: nowait postinstall skipifsilent
