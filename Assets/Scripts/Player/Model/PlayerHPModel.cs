using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerHPModel : BaseModel
{
    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
