// C# example
using UnityEditor;
using UnityEngine.Windows;
using UnityEngine;
class XRAutobuilder
{
    private static string[] scenes = { "Assets/XR Support/Scenes/InteractableCubeScene.unity" };

    [MenuItem("File/AutoBuilder/Android")]
    static void AndroidBuild()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        string buildPath = "XRBuild/XRAndroid/";

        // Create build folder if not yet exists
        if (!Directory.Exists(buildPath)){
            Directory.CreateDirectory(buildPath);
        }

        string appPath = buildPath + Application.productName + "-" + Application.version + "-Android.apk";

        BuildPipeline.BuildPlayer(scenes, appPath, BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("File/AutoBuilder/Win64")]
    static void Win64Build()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        string buildPath = "XRBuild/XRPC/Win64/";

        // Create build folder if not yet exists
        if (!Directory.Exists(buildPath)){
            Directory.CreateDirectory(buildPath);
        }

        string appPath = buildPath + Application.productName + ".exe";

        BuildPipeline.BuildPlayer(scenes, appPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}