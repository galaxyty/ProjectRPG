using Cysharp.Threading.Tasks;
using UnityEngine;

public class LobbyInitializer : BaseSceneInitializer
{
    [SerializeField]
    private Transform _uiTransform;

    public override async UniTask InitializationAsync()
    {
        Debug.Log("로비 씬 초기화...");

        // 메인 메뉴 팩토리.
        MainMenuFactory mainMenuFactory = new(_uiTransform);

        // 플레이어 스탯 팩토리.
        //PlayerStatFactory playerStatFactory = new();

        await mainMenuFactory.CreateAsync();
        //await playerStatFactory.CreateAsync();
    }
}
