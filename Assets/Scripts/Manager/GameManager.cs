using Cysharp.Threading.Tasks;
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

        // 씬 정보 초기화.
        await _sceneInitializer.InitializationAsync();

        Debug.Log("게임 매니저 시작 완료!");
    }
}