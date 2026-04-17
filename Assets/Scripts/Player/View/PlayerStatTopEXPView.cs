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

    /// <summary>
    /// EXP¹Ù º¯°æ.
    /// </summary>    
    public void SetEXP(float percent)
    {
        _expSlider.value = percent;
    }
}
