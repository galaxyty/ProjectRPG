using UnityEditor;

public class BuildScript
{
    public static void BuildAndroid()
    {
        string[] scenes = { "Assets/Scenes/Lobby.unity" };

        BuildPipeline.BuildPlayer(
            scenes,
            "Builds/MyGame.apk",
            BuildTarget.Android,
            BuildOptions.None
        );
    }
}
