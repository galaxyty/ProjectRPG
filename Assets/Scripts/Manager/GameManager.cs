using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("씬 초기화 스크립트")]
    [SerializeField]
    private BaseSceneInitializer _sceneInitializer;

    // 게임 진입점.
    private async UniTask Start()
    {
        Debug.Log("게임 매니저 시작...");

        // 60 프레임.
        Application.targetFrameRate = 60;

        // 로그인 정보 확인 (추후 추가 예정).

        // 팝업 매니저 로컬 초기화.
        await PopupManager.Instance.InitializationLocal();

        //PopupManager.Instance.Show<ResourceDownloadPopup>();

        // 리소스 다운로드.
        try
        {
            await ResourceManager.Instance.Download();
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

        // 씬 정보 초기화.
        await _sceneInitializer.InitializationAsync();

        Debug.Log("게임 매니저 시작 완료!");
    }
}