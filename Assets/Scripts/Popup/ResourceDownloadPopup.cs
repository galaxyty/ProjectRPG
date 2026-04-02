using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDownloadPopup : BasePopup
{
    [SerializeField]
    private Text _txtSize;

    [SerializeField]
    private Button _btnOK;

    [SerializeField]
    private Button _btnCancel;

    // 리소스 다운로드 퍼센트.
    private BindableReactiveProperty<string> _size;

    /// <summary>
    /// 버튼 결과 비동기 응답.
    /// </summary>
    public UniTaskCompletionSource<bool> Result = new();

    void Awake()
    {         
        // 데이터 바인딩.
        _size = ResourceManager.Instance.Size.ToBindableReactiveProperty();

        _size
            .Subscribe(size =>
            {
                _txtSize.text = size;
            })
            .AddTo(this);

        _btnOK.OnClickAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("번들 다운로드 시작 버튼 눌림");

                Result?.TrySetResult(true);

                Close();
            })
            .AddTo(this);

        _btnCancel.OnClickAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("번들 다운로드 취소 버튼 눌림");

                Result?.TrySetResult(false);

                Close();
            })
            .AddTo(this);
    }
}