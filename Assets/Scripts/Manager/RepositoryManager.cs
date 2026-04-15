
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
    public void Initialization()
    {
        CreatePlayerHPModelRepository();
        CreateStatModelRepository();
    }

    // Player HP 모델 리포지토리 생성.
    private void CreatePlayerHPModelRepository()
    {
        PlayerHPModel model = new(DataManager.Instance.StatData);

        _playerHPModelRepository.Add(model);
    }

    // Stat 모델 리포지토리 생성.
    private void CreateStatModelRepository()
    {
        var list = TableManager.Instance.StatTableDatas;

        foreach (var data in list)
        {
            StatModel model = new(data);

            _statModelRepository.Add(data.INDEX, model);
        }
    }
}
