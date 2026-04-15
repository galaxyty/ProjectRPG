public class PlayerHPModelRepository
{
    private PlayerHPModel _model;

    /// <summary>
    /// 리포지토리 모델 추가.
    /// </summary>
    public void Add(PlayerHPModel model)
    {
        _model = model;
    }

    /// <summary>
    /// 해당 Index 모델 반환.
    /// </summary>
    public PlayerHPModel Get() => _model;
}
