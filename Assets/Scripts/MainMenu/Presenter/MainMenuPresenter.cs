using UnityEngine;
using R3;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class MainMenuPresenter : BasePresenter<MainMenuView, MainMenuModel>
{
    public override void Initialization()
    {
        Debug.Log("MainMenuPresenter Initialization È£Ãâ");

        _view.OnNext
            .Subscribe(_ =>
            {
                SceneLoadManager.Instance.LoadScene("TestScene").Forget();
            });
    }

    protected override void OnBindModel()
    {
        
    }
}
