using System.Collections.Generic;

public class PlayerModelRepository
{
    private Dictionary<int, PlayerStatModel> _models = new();

    public void Add(int id, PlayerStatModel model)
    {
        _models.Add(id, model);
    }

    public PlayerStatModel Get(int id) => _models[id];
}
