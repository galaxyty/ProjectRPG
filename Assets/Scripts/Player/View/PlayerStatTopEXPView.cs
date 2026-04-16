using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatTopEXPView : BaseView
{
    [SerializeField]
    private Slider _expSlider;

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
