using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MyBuilder {
    [MenuItem("Tools/Build Android")]
    public static void BuildAndroid() {
        InitOptions();
        var buildScenes = GetBuildScenes();
        var binaryPath = GetBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        BuildPipeline.BuildPlayer(
            buildScenes,
            binaryPath + ".apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }

    [MenuItem("Tools/Build Mac")]
    public static void BuildMac() {
        InitOptions();
        var buildScenes = GetBuildScenes();
        var binaryPath = GetBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        BuildPipeline.BuildPlayer(
            buildScenes,
            binaryPath + ".app",
            BuildTarget.StandaloneOSX,
            BuildOptions.None
        );
    }

    [MenuItem("Tools/Build Linux")]
    public static void BuildLinux() {
        InitOptions();
        var buildScenes = GetBuildScenes();
        var binaryPath = GetBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinuxUniversal);
        BuildPipeline.BuildPlayer(
            buildScenes,
            binaryPath,
            BuildTarget.StandaloneLinuxUniversal,
            BuildOptions.None
        );
    }

    [MenuItem("Tools/Build Windows")]
    public static void BuildWindows() {
        InitOptions();
        var buildScenes = GetBuildScenes();
        var binaryPath = GetBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone,
            BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildPlayer(
            buildScenes,
            binaryPath + "-Windows64/RBS-Client.exe",
            BuildTarget.StandaloneWindows64,
            BuildOptions.None
        );
    }

    [MenuItem("Tools/Build Xcode")]
    public static void BuildXcode() {
        InitOptions();
        var buildScenes = GetBuildScenes();
        var binaryPath = GetBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        BuildPipeline.BuildPlayer(
            buildScenes,
            binaryPath + "-Xcode",
            BuildTarget.iOS,
            BuildOptions.None
        );
    }

    private static void InitOptions() {
        PlayerSettings.applicationIdentifier = "com.rarudo.tyariai";
        PlayerSettings.statusBarHidden = true;
    }

    private static string GetBuildPath() {
        var binaryName = Environment.GetEnvironmentVariable("BINARY_NAME"); // test
        var binaryPath = Environment.GetEnvironmentVariable("BINARY_PATH"); // hoge/
        return binaryPath + binaryName; // hoge/test
    }

    private static string[] GetBuildScenes() {
        return EditorBuildSettings.scenes
            .Where(x => x.enabled)
            .Select(x => x.path)
            .ToArray();
    }
}