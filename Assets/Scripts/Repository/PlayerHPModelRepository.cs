using System.Collections.Generic;

public class PlayerHPModelRepository
{
    private Dictionary<int, PlayerHPModel> _models = new();

    /// <summary>
    /// 리포지토리 모델 추가.
    /// </summary>
    public void Add(int index, PlayerHPModel model)
    {
        _models.Add(index, model);
    }

    /// <summary>
    /// 해당 Index 모델 반환.
    /// </summary>
    public PlayerHPModel Get(int index) => _models[index];
}
