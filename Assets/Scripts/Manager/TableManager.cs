using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : BaseObjectSingleton<TableManager>
{
    private List<StatTableData> _statTableDatas;
    
    // 프로퍼티.
    public List<StatTableData> StatTableDatas
    {
        get { return _statTableDatas; }
        private set { }
    }

    /// <summary>
    /// 테이블 매니저 초기화.
    /// </summary>
    public async UniTask InitializationAsync()
    {
        // 동시에 초기화.
        var statTask = LoadStatTable();

        // 모든 작업이 끝날 때까지 대기.
        await UniTask.WhenAll(statTask);
    }

    // STAT 테이블 생성.
    private async UniTask LoadStatTable()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_STAT);
        _statTableDatas = JsonConvert.DeserializeObject<List<StatTableData>>(json.text);
    }
}
