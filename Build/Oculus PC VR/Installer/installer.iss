; -- XR Support --
; Inno installer script for XR Support
; MUST be installed on x64 bit machine

#define AppName "XR Support"
#define AppVersion "0.1"
#define CompanyName "KnightVR"
#define InstallerName "XRSupport"
#define ExeName "XRSupport.exe"
#define IconFile "kvr_logo.ico"
#define IconFolder "../../../Assets/KVR/Logo/"
#define BuildFolder "../Release/Win64/"
#define LicenseFile "../../../LICENSE"

[Setup]
AppId={{C2F89D05-D98B-4389-8B20-18060FD648CA}}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#CompanyName}
AppPublisherURL=https://github.com/KnightVR
AppSupportURL=https://github.com/KnightVR/XRSupport
AppUpdatesURL=https://github.com/KnightVR/XRSupport/releases
DefaultDirName={pf64}/{#CompanyName}/{#AppName}
DefaultGroupName={#CompanyName}
LicenseFile={#LicenseFile}
OutputBaseFilename={#InstallerName}-{#AppVersion}-Win64
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
Source: "{#BuildFolder}*"; DestDir: "{app}"; Flags: ignoreversion createallsubdirs recursesubdirs
Source: "{#IconFolder}{#IconFile}"; DestDir: "{app}"

[Run]


[Icons]
Name: "{group}\{cm:UninstallProgram,{#AppName}}"; Filename: "{uninstallexe}"
Name: "{group}\{#AppName}"; Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#IconFile}"
Name: "{commondesktop}\{#AppName}"; Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#IconFile}"; Tasks: desktopicon