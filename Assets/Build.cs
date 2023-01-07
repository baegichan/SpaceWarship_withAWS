using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Build : MonoBehaviour
{
    [MenuItem("Build/Standalone Windows")]
    public static void PerformBuild()
    {
        BuildPlayerOptions options = new BuildPlayerOptions();
        // ¾À Ãß°¡
        List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            scenes.Add(scene.path);
        }
        options.scenes = scenes.ToArray();

        // Å¸°Ù °æ·Î(ºôµå °á°ú¹°ÀÌ ¿©±â »ý¼ºµÊ)
        string path = "C:/Build/FirstPerson.exe";
        // ºôµå Å¸°Ù
        options.locationPathName = "Build/FirstPerson.exe";
        options.target = BuildTarget.StandaloneWindows;

        // ºôµå
        BuildPipeline.BuildPlayer(options);
    }
}