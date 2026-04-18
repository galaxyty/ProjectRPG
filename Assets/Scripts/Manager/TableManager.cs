using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : BaseObjectSingleton<TableManager>
{
    public List<StatTableData> StatTableDatas { get; private set; }
    public List<StageTableData> StageTableDatas { get; private set; }
    public List<SkillTableData> SkillTableDatas { get; private set; }
    public List<MonsterGroupTableData> MonsterGroupTableDatas { get; private set; }

    /// <summary>
    /// 테이블 매니저 초기화.
    /// </summary>
    public override async UniTask InitializationAsync()
    {
        // 동시에 초기화.
        var statTask = LoadStatTable();
        var stageTask = LoadStageTable();
        var skillTask = LoadSkillTable();
        var monsterGroup = LoadMonsterGroupTable();

        // 모든 작업이 끝날 때까지 대기.
        await UniTask.WhenAll(
            statTask,
            stageTask,
            skillTask,
            monsterGroup
            );
    }

    // STAT 테이블 생성.
    private async UniTask LoadStatTable()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_STAT);
        StatTableDatas = JsonConvert.DeserializeObject<List<StatTableData>>(json.text);
    }

    // STAGE 테이블 생성.
    private async UniTask LoadStageTable()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_STAGE);
        StageTableDatas = JsonConvert.DeserializeObject<List<StageTableData>>(json.text);
    }

    // SKILL 테이블 생성.
    private async UniTask LoadSkillTable()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_SKILL);
        SkillTableDatas = JsonConvert.DeserializeObject<List<SkillTableData>>(json.text);
    }

    // MONSTER_GROUP 생성.
    private async UniTask LoadMonsterGroupTable()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_MONSTER_GROUP);
        MonsterGroupTableDatas = JsonConvert.DeserializeObject<List<MonsterGroupTableData>>(json.text);
    }
}
