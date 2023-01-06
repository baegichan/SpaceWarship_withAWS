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
        // �� �߰�
        List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            scenes.Add(scene.path);
        }
        options.scenes = scenes.ToArray();

        // Ÿ�� ���(���� ������� ���� ������)
        string path = "/home/ec2-user/BUILDTARGET/Server.exe";
        // ���� Ÿ��
        options.target = BuildTarget.StandaloneWindows;

        // ����
        BuildPipeline.BuildPlayer(scenes.ToArray(), path, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}