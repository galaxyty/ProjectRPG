using R3;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoadView : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Text _txtPercent;
    
    // 리소스 다운로드 퍼센트.
    private BindableReactiveProperty<float> _percent;
    private BindableReactiveProperty<string> _percentString;

    void Awake()
    {
        // 데이터 바인딩.
        _percent = ResourceManager.Instance.Percent.ToBindableReactiveProperty();
        _percentString = ResourceManager.Instance.PercentString.ToBindableReactiveProperty();

        _percent
            .Subscribe(percent =>
            {
                _slider.value = percent;
            })
            .AddTo(this);

        _percentString
            .Subscribe(percentString =>
            {
                _txtPercent.text = percentString;
            })
            .AddTo(this);
    }
}
