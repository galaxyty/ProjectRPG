using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerStatTopIconView : BaseView<PlayerStatTopIconPresenter>
{
    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
