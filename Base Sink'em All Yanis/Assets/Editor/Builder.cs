using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class Builder : MonoBehaviour {

    [MenuItem("Build/Build PC server")]
    public static void MyBuildServer()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Project/Scenes/Server.unity" };
        buildPlayerOptions.locationPathName = "Build/Serveur/Server.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("Build/Build PC client")]
    public static void MyBuildClient()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Project/Scenes/MenuReseau.unity", "Assets/Project/Scenes/LoadingScene.unity", "Assets/Project/Scenes/Game.unity" };
        buildPlayerOptions.locationPathName = "Build/Client/Client.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }


    }


    [MenuItem("Build/Build PC client + server")]
    public static void MyBuildBothPc()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Project/Scenes/MenuReseau.unity", "Assets/Project/Scenes/LoadingScene.unity", "Assets/Project/Scenes/Game.unity" };
        buildPlayerOptions.locationPathName = "Build/Client/Client.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

        BuildPlayerOptions buildPlayerOptions1 = new BuildPlayerOptions();
        buildPlayerOptions1.scenes = new[] { "Assets/Project/Scenes/Server.unity" };
        buildPlayerOptions1.locationPathName = "Build/Serveur/Server.exe";
        buildPlayerOptions1.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions1.options = BuildOptions.None;
        BuildReport report1 = BuildPipeline.BuildPlayer(buildPlayerOptions1);
        BuildSummary summary1 = report1.summary;

        if (summary1.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary1.totalSize + " bytes");
        }

        if (summary1.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }

    }


    [MenuItem("Build/Build Android client")]
    public static void MyBuildClientAndroid()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Project/Scenes/MenuReseau.unity", "Assets/Project/Scenes/Game.unity" };
        buildPlayerOptions.locationPathName = "Build/Client/Game.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
