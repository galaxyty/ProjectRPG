using UnityEngine;

public class RepositoryManager : BaseObjectSingleton<RepositoryManager>
{
    private PlayerStatModelRepository _playerStatModelRepository = new();

    // 프로퍼티
    public PlayerStatModelRepository PlayerStatModelRepository
    {
        get { return _playerStatModelRepository; }
        private set { }
    }

    /// <summary>
    /// 리포지토리 매니저 초기화.
    /// </summary>
    public void Initialization()
    {
        CreatePlayerStatRepository();
    }

    // Player Stat 모델 생성.
    private void CreatePlayerStatRepository()
    {
        var data = TableManager.Instance.StatTableDatas.Find(data => data.LEVEL == DataManager.Instance.StatData.Level.Value);

        // null 체크.
        if (data == null)
        {
            Debug.LogError($"Stat Table 데이터를 조회할 수 없습니다");
            return;
        }

        PlayerStatModel model = new(DataManager.Instance.StatData, data);

        _playerStatModelRepository.Add(model);
    }
}
