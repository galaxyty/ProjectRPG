using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneView : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    /// <summary>
    /// 진행률 바 조정.
    /// </summary>
    public void SetProgress(float percent)
    {
        _slider.value = percent;
    }
}
