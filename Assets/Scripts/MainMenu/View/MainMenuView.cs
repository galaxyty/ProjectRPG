using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : BaseView<MainMenuPresenter>
{
    [SerializeField]
    private Button _btnNext;

    public Observable<Unit> OnNext => _btnNext.OnClickAsObservable();

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
