using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BootSceneInitializer : BaseSceneInitializer
{
    public override async UniTask InitializationAsync()
    {
        await Addressables.InitializeAsync();

        // 60 프레임.
        Application.targetFrameRate = 60;

        // 로그인 정보 확인 (추후 추가 예정).

        // 팝업 매니저 로컬 초기화.
        await PopupManager.Instance.InitializationLocal();

        // 리소스 다운로드.
        try
        {
            await ResourceManager.Instance.CheckDownloadAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"GameManager Start 중, 리소스 다운로드 중 에러 발생! : {e}");
            return;
        }

        // 각 매니저들 동시에 초기화.
        var audioTask = AudioManager.Instance.InitializationAsync();
        var sceneTask = SceneLoadManager.Instance.InitializationAsync();

        // 두 작업이 모두 완료될 때까지 대기.
        await UniTask.WhenAll(audioTask, sceneTask);

        SceneLoadManager.Instance.LoadScene(Consts.kSCENE_LOBBY).Forget();
    }
}
