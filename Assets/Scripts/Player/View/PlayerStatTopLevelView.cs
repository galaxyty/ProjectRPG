using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatTopLevelView : BaseView
{
    [SerializeField]
    private Text _txtLevel;

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
