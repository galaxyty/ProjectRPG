using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestSceneInitializer : BaseSceneInitializer
{
    public override async UniTask InitializationAsync()
    {
        Debug.Log("TestSceneInitializer 초기화");

        // 몬스터매니저 초기화.
        await MonsterManager.Instance.Initialization();
        
        BGMSoundBus.OnBGM?.OnNext(AudioManager.Instance.GetClip(Consts.kAUDIO_MAIN));
    }
}
