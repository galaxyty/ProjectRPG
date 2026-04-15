using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

public class RepositoryManager : BaseObjectSingleton<RepositoryManager>
{
    private PlayerHPModelRepository _playerHPModelRepository = new();
    private StatModelRepository _statModelRepository = new();

    public PlayerHPModelRepository PlayerHPModelRepository
    {
        get { return _playerHPModelRepository; }
        private set { }
    }
    public StatModelRepository StatModelRepository
    {
        get { return _statModelRepository; }
        private set { }
    }

    /// <summary>
    /// 리포지토리 매니저 초기화.
    /// </summary>
    public async UniTask InitializationAsync()
    {
        CreatePlayerHPModelRepository();
        await CreateStatModelRepository();
    }

    // Player HP 모델 리포지토리 생성.
    private void CreatePlayerHPModelRepository()
    {
        PlayerHPModel model = new(DataManager.Instance.StatData);

        _playerHPModelRepository.Add(model);
    }

    // Stat 모델 리포지토리 생성.
    private async UniTask CreateStatModelRepository()
    {
        var json = await ResourceManager.Instance.LoadAsync<TextAsset>(Consts.kPATH_JSON_STAT);
        var list = JsonConvert.DeserializeObject<List<StatTableData>>(json.text);

        foreach (var data in list)
        {
            StatModel model = new(data);

            _statModelRepository.Add(data.INDEX, model);
        }
    }
}
