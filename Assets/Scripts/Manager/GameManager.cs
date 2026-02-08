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

        // 로그인 정보 확인 (추후 추가 예정).       


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

        // 로딩 싱글톤 초기화.
        await SceneLoadManager.Instance.InitializationAsync();

        // 씬 정보 초기화.
        await _sceneInitializer.InitializationAsync();

        Debug.Log("게임 매니저 시작 완료!");
    }
}