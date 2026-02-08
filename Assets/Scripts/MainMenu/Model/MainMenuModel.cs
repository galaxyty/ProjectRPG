using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuModel : BaseModel
{
    public override UniTask InitializationAsync()
    {
        Debug.Log("MainMenuModel InitializationAsync »£√‚");

        return UniTask.CompletedTask;
    }
}
