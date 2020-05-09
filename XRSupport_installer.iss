; -- XRSupport --
; Inno installer script for XRSupport
; MUST be installed on x64 bit machine

#define AppName "XRSupport"
#define AppVersion "0.1"
#define CompanyName "KnightVR"
#define InstallerName "XRSupport"
#define ExeName "XRSupport.exe" 
#define IconFile "kvr_logo.ico"
#define IconFolder "../../Logo/"
#define BuildFolder "../Win64/"
#define LicenseFile "../../LICENSES/MIT/LICENSE"

[Setup]
AppId={{"XRSupport0.1"}}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#CompanyName}
AppPublisherURL=https://github.com/{#CompanyName}
AppSupportURL=https://github.com/{#CompanyName}/{#AppName}
AppUpdatesURL=https://github.com/{#CompanyName}/{#AppName}/releases
DefaultDirName={pf64}/{#CompanyName}/{#AppName}
DefaultGroupName={#CompanyName}
LicenseFile={#LicenseFile}
OutputBaseFilename={#InstallerName}-{#AppVersion}-Win64-Installer
SetupIconFile={#IconFolder}{#IconFile}
Compression=lzma2
SolidCompression=yes
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
UninstallDisplayIcon={#IconFolder}{#IconFile}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#BuildFolder}*"; Excludes: "*.gitkeep"; DestDir: "{app}"; Flags: ignoreversion createallsubdirs recursesubdirs
Source: "{#IconFolder}{#IconFile}"; DestDir: "{app}"

[Run]


[Icons]
Name: "{group}\{cm:UninstallProgram,{#AppName}}"; Filename: "{uninstallexe}"
Name: "{group}\{#AppName}"; Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#IconFile}"
Name: "{commondesktop}\{#AppName}"; Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#IconFile}"; Tasks: desktopicon

