using Cysharp.Threading.Tasks;
using UnityEngine;

public class StageSceneInitializer : BaseSceneInitializer
{
    [SerializeField]
    private Transform _uiTransform;

    public override async UniTask InitializationAsync()
    {
        Debug.Log("TestSceneInitializer 초기화");

        // 플레이어 상단 팩토리.
        PlayerStatTopFactory playerStatTopFactory = new(_uiTransform);
        await playerStatTopFactory.CreateAsync();

        // 몬스터매니저 초기화.
        await MonsterManager.Instance.Initialization();
        
        // BGM 재생.
        BGMSoundBus.OnBGM?.OnNext(AudioManager.Instance.GetClip(Consts.kAUDIO_MAIN));
    }
}
