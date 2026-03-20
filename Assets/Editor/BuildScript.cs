using System.Linq;
using UnityEditor;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string[] scenes = EditorBuildSettings.scenes
        .Where(scene => scene.enabled)   // Ã¼Å©µÈ ¾À¸¸
        .Select(scene => scene.path)
        .ToArray();

        BuildPipeline.BuildPlayer(
            scenes,
            "Builds/MyGame.apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
