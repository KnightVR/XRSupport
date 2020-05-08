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
        string buildPath = "XRBuild/XRAndroid/" + Application.productName + "-" + Application.version + "-Android.apk";

        // Create build folder if not yet exists
        Directory.CreateDirectory(buildPath);

        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("File/AutoBuilder/Win64")]
    static void Win64Build()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        string buildPath = "XRBuild/XRPC/Win64/" + Application.productName + ".exe";

        // Create build folder if not yet exists
        Directory.CreateDirectory(buildPath);

        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}