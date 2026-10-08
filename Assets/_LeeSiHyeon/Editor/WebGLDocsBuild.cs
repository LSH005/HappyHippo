using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class WebGLDocsBuild
{
    public static void Build()
    {
        string projectRoot = Path.GetDirectoryName(Application.dataPath);
        string output = Path.Combine(projectRoot, "docs");
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = output,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("WebGL docs 빌드 실패: " + report.summary.result);
            EditorApplication.Exit(1);
            return;
        }

        Debug.Log("WebGL docs 빌드 완료: " + output);
    }
}
