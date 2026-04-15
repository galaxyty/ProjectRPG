using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerHPFactory : IFactory
{
    // UI Äµ¹ö½º Æ®·£½ºÆû.
    private Transform _uiParent;

    public PlayerHPFactory(Transform ui)
    {
        _uiParent = ui;
    }

    public async UniTask CreateAsync()
    {
        Debug.Log("PlayerHPFactory ÆÑÅä¸® »ý¼º");

        var uiHPLoad = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_PLAYER_HP_VIEW);
        var prefab = Object.Instantiate(uiHPLoad, _uiParent);

        var view = prefab.GetComponent<PlayerHPView>();

        var model = RepositoryManager.Instance.PlayerHPModelRepository.Get();

        await model.InitializationAsync();

        PlayerHPPresenter presenter = new(model);

        presenter.SetModel(model);
        presenter.SetView(view);
        presenter.Initialization();
    }
}
