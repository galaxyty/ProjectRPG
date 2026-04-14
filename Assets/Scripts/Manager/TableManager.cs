using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : BaseObjectSingleton<TableManager>
{
    private StatTableDataRepository _statTableDataRepository = new();

    public StatTableDataRepository StatTableDataRepository
    {
        get { return _statTableDataRepository; }
        private set { }
    }

    /// <summary>
    /// 테이블 매니저 초기화.
    /// </summary>
    public async UniTask InitializationAsync()
    {
        await LoadPlayerStat();
    }

    // Player Stat 테이블 데이터 로드.
    private async UniTask LoadPlayerStat()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_STAT);
        var list = JsonConvert.DeserializeObject<List<StatTableData>>(json.text);

        foreach (var data in list)
        {
            _statTableDataRepository.Add(data.INDEX, data);
        }
    }
}