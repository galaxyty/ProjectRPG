using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatView : BaseView
{    
    [SerializeField]
    private Button _btnClick;

    public Observable<Unit> OnTest => _btnClick.OnClickAsObservable();

    public override UniTask InitializationAsync()
    {
        //_btnClick.onClick.AddListener(_pre);

        return UniTask.CompletedTask;
    }    
}
