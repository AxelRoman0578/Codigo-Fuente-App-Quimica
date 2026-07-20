using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildScript
{
    [MenuItem("Build/Build iOS")]
    public static void BuildiOS()
    {
        string buildPath = "Builds/iOS";
        
        if (!Directory.Exists("Builds"))
        {
            Directory.CreateDirectory("Builds");
        }
        
        string[] scenes = GetScenes();
        if (scenes.Length == 0)
        {
            Debug.LogError("No active scenes found in Build Settings!");
            return;
        }

        Debug.Log("Starting iOS Build to: " + buildPath);
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildResult result = report.summary.result;

        if (result == BuildResult.Succeeded)
        {
            Debug.Log("iOS Build Succeeded!");
        }
        else
        {
            Debug.LogError("iOS Build Failed!");
        }
    }

    private static string[] GetScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        var activeScenes = new System.Collections.Generic.List<string>();
        foreach (var scene in scenes)
        {
            if (scene.enabled)
            {
                activeScenes.Add(scene.path);
            }
        }
        return activeScenes.ToArray();
    }
}
