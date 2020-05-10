// C# example
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Diagnostics;

class XRSupportBuilder
{
    // Edit these to match your project!
    // Set this to the scenes to include in the build
    private static string[] scenes = { "Assets/XRSupport/Scenes/InteractableCubeScene.unity" };
    // Set this to the folder where you would like Android to be built
    private static string buildFolderAndroid = "Build/Android/";
    // Set this to the folder where you would like Windows 64 to be built
    private static string buildFolderWin64 = "Build/Win64/";
    // Set this to the folder path of an app icon file (used in windows installer)
    private static string logoFolder = "Assets/XRSupport/Icon/";
    // Set this to the name of an app icon file (used in windows installer)
    private static string logoFile = "logo.ico";
    // Set this to the path of your license file (used in windows installer)
    private static string licenseFile = "Assets/XRSupport/LICENSE";
    // Set this to the install location of Inno Setup (download from here https://jrsoftware.org/isdl.php)
    // By default will assume it is in 'C:/Users/USERNAME/AppData/Local/Programs/Inno Setup 6/'
    private static string innoSetupFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/../Local/Programs/Inno Setup 6/";

    [MenuItem("File/XRSupport/Build Android")] //Menu item to appear in the Unity editor
    //Build Android APK (Will automatically switch to Android build target)
    static void BuildAndroid()
    {
        // Switch to Android build target
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        // Use class build folder for android (defined at the top)
        string buildPath = buildFolderAndroid;

        // Create build folder if not yet exists
        if (!Directory.Exists(buildPath)){
            Directory.CreateDirectory(buildPath);
        }

        // Define apk name
        string appPath = buildPath + Application.productName + "-" + Application.version + "-Android.apk";

        // Build application
        BuildPipeline.BuildPlayer(scenes, appPath, BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("File/XRSupport/Build Win64")] //Menu item to appear in the Unity editor
    //Build Windows 64 application (Will automatically switch to Windows 64 build target)
    static void BuildWin64()
    {
        // Switch to Windows 64 build target
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        // Application is built in 'Release' so that it is in a known folder for inno setup later on
        // Use class build folder for win64 (defined at the top)
        string buildPath = buildFolderWin64 + "Release/";

        // Create build folder if not yet exists
        if (!Directory.Exists(buildPath)){
            Directory.CreateDirectory(buildPath);
        }
        // Define exe name
        string appPath = buildPath + Application.productName + ".exe";

        // Build application
        BuildPipeline.BuildPlayer(scenes, appPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("File/XRSupport/Build Win64 Installer")] //Menu item to appear in the Unity editor
    //Build Windows 64 Installer (Uses Inno Setup Tool so make sure the path to the install locaiton is set (innoSetupFolder))
    static void BuildWin64Installer()
    {
        // Make sure 'BuildWin64' is run before trying to build the installer
        // Get the project folder
        string projFolder = Application.dataPath + "/../";
        // Define inno installer script file name
        string installerScriptFile = Application.productName + "_installer.iss";
        // Define installer build path
        string installerScriptFolder = buildFolderWin64 + "Installer/";
        // Define absolute folder path to application build folder
        string absBuildFolder = projFolder + buildFolderWin64 + "Release/";
        // Define absolute folder path to logo icon file
        string absLogoFolder = projFolder + logoFolder;
        // Define absolute file path to license file
        string absLicenseFile = projFolder + licenseFile;
        // Define absilute file path to inno installer script
        string absInstallerScriptFile = projFolder + installerScriptFolder + installerScriptFile;

        // Create text for inno script using application properties
        string appid = Application.productName + Application.version;
        string[] inno_installer_script = {
            "; Inno installer script for " + Application.productName,
            "; MUST be installed on x64 bit machine",
            "#define AppName \"" + Application.productName + "\"",
            "#define AppVersion \"" + Application.version + "\"",
            "#define CompanyName \"" + Application.companyName + "\"",
            "#define InstallerName \"" + Application.productName + "\"",
            "#define ExeName \"" + Application.productName + ".exe\"",
            "#define IconFile \"" + logoFile + "\"",
            "#define IconFolder \"" + absLogoFolder + "\"",
            "#define BuildFolder \"" + absBuildFolder + "\"",
            "#define LicenseFile \"" + absLicenseFile + "\"",
            "[Setup]",
            "AppId ={{\"" + appid + "\"}}",
            "AppName ={#AppName}",
            "AppVersion ={#AppVersion}",
            "AppPublisher ={#CompanyName}",
            "AppPublisherURL = https://github.com/{#CompanyName}",
            "AppSupportURL = https://github.com/{#CompanyName}/{#AppName}",
            "AppUpdatesURL = https://github.com/{#CompanyName}/{#AppName}/releases",
            "DefaultDirName ={pf64}/{#CompanyName}/{#AppName}",
            "DefaultGroupName ={#CompanyName}",
            "LicenseFile ={#LicenseFile}",
            "OutputBaseFilename ={#InstallerName}-{#AppVersion}-Win64-Installer",
            "SetupIconFile ={#IconFolder}{#IconFile}",
            "Compression = lzma2",
            "SolidCompression = yes",
            "ArchitecturesAllowed = x64",
            "ArchitecturesInstallIn64BitMode = x64",
            "UninstallDisplayIcon ={#IconFolder}{#IconFile}",
            "[Languages]",
            "Name: \"english\"; MessagesFile: \"compiler:Default.isl\"",
            "[Tasks]",
            "Name: \"desktopicon\"; Description: \"{cm:CreateDesktopIcon}\"; GroupDescription: \"{cm:AdditionalIcons}\"; Flags: unchecked",
            "[Files]",
            "Source: \"{#BuildFolder}*\"; Excludes: \"*.gitkeep\"; DestDir: \"{app}\"; Flags: ignoreversion createallsubdirs recursesubdirs",
            "Source: \"{#IconFolder}{#IconFile}\"; DestDir: \"{app}\"",
            "[Run]",
            "[Icons]",
            "Name: \"{group}\\{cm:UninstallProgram,{#AppName}}\"; Filename: \"{uninstallexe}\"",
            "Name: \"{group}\\{#AppName}\"; Filename: \"{app}\\{#ExeName}\"; IconFilename: \"{app}\\{#IconFile}\"",
            "Name: \"{commondesktop}\\{#AppName}\"; Filename: \"{app}\\{#ExeName}\"; IconFilename: \"{app}\\{#IconFile}\"; Tasks: desktopicon",
        };

        // Create installer script folder if not yet exists
        if (!Directory.Exists(installerScriptFolder))
        {
            UnityEngine.Debug.Log("Creating folder: " + installerScriptFolder);
            Directory.CreateDirectory(installerScriptFolder);
        }
        // Delete file if it already exists
        if (File.Exists(absInstallerScriptFile))
        {
            UnityEngine.Debug.Log("Clearing previous installer script: " + absInstallerScriptFile);
            File.Delete(absInstallerScriptFile);
        }
        // Write inno setup script text to file
        using (StreamWriter sw = File.AppendText(absInstallerScriptFile))
        {
            foreach (var line in inno_installer_script)
            {
                sw.WriteLine(line);
            }
        }

        // Define path to compile app in inno setup
        string innoCompileApp = innoSetupFolder + "Compil32.exe";

        UnityEngine.Debug.Log("Inno Setup location: " + innoCompileApp);

        // Start Inno Setup compiler process (runs externally so will exit immediately)
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = innoCompileApp;
        startInfo.Arguments = "/cc " + absInstallerScriptFile;
        Process.Start(startInfo);
    }

    [MenuItem("File/XRSupport/Build All")] //Menu item to appear in the Unity editor
    // Run all build processes in turn
    static void BuildAll()
    {
        BuildWin64(); // Make sure this is run before the installer as it requires the build files to create the installer
        BuildWin64Installer();
        BuildAndroid();
    }
}