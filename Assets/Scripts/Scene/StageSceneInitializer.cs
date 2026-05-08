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

        // 현재 스테이지 데이터.
        var stageLevel = DataManager.Instance.StageUserData.StageLevel.Value;
        var stageData = TableManager.Instance.StageTableDatas.Find(data => data.INDEX == stageLevel);
        var monsterDatas = TableManager.Instance.MonsterGroupTableDatas;

        // 몬스터매니저 초기화.
        await MonsterManager.Instance.Initialization(monsterDatas);
        
        // BGM 재생.
        BGMSoundBus.OnBGM?.OnNext(AudioManager.Instance.GetClip(Consts.kAUDIO_MAIN));
    }
}
