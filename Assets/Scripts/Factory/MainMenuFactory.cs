using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainMenuFactory : IFactory
{
    public async UniTask CreateAsync()
    {
        Debug.Log("UIMainMenu ÆÑÅä¸® »ý¼º");

        // Äµ¹ö½º Root.
        Transform uiRoot = UIManager.Instance.GetRoot(UIManager.CanvasType.UI);

        // ·Îºñ ¾À¿¡¼­ »ý¼º ½ÃÅ³ ÇÁ¸®ÆÕ.
        GameObject UIMainMenu = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_MAIN_MENU_VIEW);

        // ÇÁ¸®ÆÕ »ý¼º.
        GameObject prefab = Object.Instantiate(UIMainMenu, uiRoot);

        // ºä.
        MainMenuView view = prefab.GetComponent<MainMenuView>();

        // ¸ðµ¨.
        MainMenuModel model = new();

        await model.InitializationAsync();

        // ÇÁ·¹Á¨Æ®.
        MainMenuPresenter presenter = new();

        presenter.SetModel(model);
        presenter.SetView(view);
        presenter.Initialization();
    }
}
