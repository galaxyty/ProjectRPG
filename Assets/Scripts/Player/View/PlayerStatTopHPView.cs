using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatTopHPView : BaseView<PlayerStatTopHPPresenter>
{
    [SerializeField]
    private Slider _hpSlider;

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// HP¹Ù º¯°æ.
    /// </summary>    
    public void SetHP(float percent)
    {
        _hpSlider.value = percent;
    }
}
