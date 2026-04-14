using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class PlayerHPModel : BaseModel
{
    public ReactiveProperty<int> HP = new();

    public PlayerHPModel(StatData statData)
    {
        HP.Value = statData.HP.Value;
    }

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
