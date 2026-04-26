
public static class Consts
{
    // 상태.
    public enum eSTATE
    {
        Idle = 0,
        Move,
        Attack
    }

    // 프리팹.
    public static string kPATH_PLAYER_STAT_VIEW = "Prefabs/PlayerStatView";
    public static string kPATH_MAIN_MENU_VIEW = "Prefabs/MainMenuView";

    // 로딩 뷰.
    public static string kPATH_LOADING_SCENE_VIEW = "Prefabs/LoadingSceneView";

    // Json.
    public static string kPATH_JSON_STAT = "Json/STAT";
    public static string kPATH_JSON_STAGE = "Json/STAGE";
    public static string kPATH_JSON_SKILL = "Json/SKILL";
    public static string kPATH_JSON_MONSTER_GROUP = "Json/MONSTER_GROUP";

    // 오브젝트.
    public static string kPATH_MONSTER_THIEF = "Prefabs/Monster/MonsterThief";
    public static string kPATH_PLAYER_STAT_TOP_VIEW = "Prefabs/UI/Player/PlayerStatTopView";

    // 애니메이터 키.
    public static string kANIMATOR_KEY_STATE = "State";

    // 오디오 경로.
    public static string kAUDIO_MAIN = "Sounds/BGM/Main";

    // 팝업 경로.
    public static string kPATH_RESOURCE_DOWNLOAD_POPUP = "Prefabs/Popup/ResourceDownloadPopup";

    // 텍스쳐 경로.
    public static string kPATH_TEXTURE_MONSTER = "Textures/Monster/Monster";

    // 어드레서블 키.
    public static string kAD_KEY_DOWNLOAD = "Download";
    public static string kAD_KEY_PREFAB = "Prefab";
    public static string kAD_KEY_SOUND = "Sound";
    public static string kAD_KEY_TEXTURE = "Texture";
    public static string kAD_KEY_ANIMATOR = "Animator";
    public static string kAD_KEY_CLIP = "Clip";
    public static string kAD_KEY_JSON = "Json";

    // 씬 이름.
    public static string kSCENE_LOBBY = "LobbyScene";
    public static string kSCENE_STAGE = "StageScene";
}
