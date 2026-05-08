using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuFactory : IFactory
{
    // UI Äµ¹ö½º Æ®·£½ºÆû.
    private Transform _uiParent;

    public MainMenuFactory(Transform ui)
    {
        _uiParent = ui;
    }

    public async UniTask CreateAsync()
    {
        Debug.Log("UIMainMenu ÆÑÅä¸® »ý¼º");

        // ·Îºñ ¾À¿¡¼­ »ý¼º ½ÃÅ³ ÇÁ¸®ÆÕ.
        GameObject UIMainMenu = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_MAIN_MENU_VIEW);

        // ÇÁ¸®ÆÕ »ý¼º.
        GameObject prefab = Object.Instantiate(UIMainMenu, _uiParent);

        // ºä.
        var view = prefab.GetComponent<MainMenuView>();

        // ¸ðµ¨.
        MainMenuModel model = new();

        await model.InitializationAsync();

        // ÇÁ·¹Á¨Æ®.
        MainMenuPresenter presenter = new();
        
        presenter.SetModelView(model, view);
        presenter.Initialization();

        // ºä¿¡ ÇÁ·¹Á¨Æ® ¼ÂÆÃ.
        view.SetPresenter(presenter);
    }
}
