using Cysharp.Threading.Tasks;
using UnityEngine;

public class LobbyInitializer : BaseSceneInitializer
{
    [SerializeField]
    private Transform _uiTransform;

    public override async UniTask InitializationAsync()
    {
        Debug.Log("로비 씬 초기화...");

        // 데이터 로드.
        DataManager.Instance.Load();

        // 리포지토리 초기화.
        RepositoryManager.Instance.Initialization();

        // 메인 메뉴 팩토리.
        MainMenuFactory mainMenuFactory = new(_uiTransform);

        await mainMenuFactory.CreateAsync();
    }
}
