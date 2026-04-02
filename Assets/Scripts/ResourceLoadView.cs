using R3;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoadView : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    
    // 리소스 다운로드 퍼센트.
    private BindableReactiveProperty<float> _percent;

    void Awake()
    {
        // 데이터 바인딩.
        _percent = ResourceManager.Instance.Percent.ToBindableReactiveProperty();

        _percent
            .Subscribe(percent =>
            {
                _slider.value = percent;
            })
            .AddTo(this);
    }
}
