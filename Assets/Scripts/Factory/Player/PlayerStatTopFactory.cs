using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerStatTopFactory : IFactory
{
    // UI 캔버스 트랜스폼.
    private Transform _uiParent;

    public PlayerStatTopFactory(Transform ui)
    {
        _uiParent = ui;
    }

    public async UniTask CreateAsync()
    {
        Debug.Log("PlayerStatTopFactory 팩토리 생성");

        // 오브젝트 생성.
        var uiTop = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_PLAYER_STAT_TOP_VIEW);
        var prefab = Object.Instantiate(uiTop, _uiParent);

        // 하위 뷰 가져오기.
        var hpView = prefab.GetComponentInChildren<PlayerStatTopHPView>();
        var expView = prefab.GetComponentInChildren<PlayerStatTopEXPView>();
        var iconView = prefab.GetComponentInChildren<PlayerStatTopIconView>();
        var levelView = prefab.GetComponentInChildren<PlayerStatTopLevelView>();

        // 모델 초기화.
        var model = RepositoryManager.Instance.PlayerStatModelRepository.Get();
        await model.InitializationAsync();

        // 각 프레젠트 생성.
        PlayerStatTopHPPresenter hpPresenter = new(model);
        PlayerStatTopEXPPresenter expPresenter = new(model);
        PlayerStatTopIconPresenter iconPresenter = new(model);
        PlayerStatTopLevelPresenter levelPresenter = new(model);

        // 각 프레젠트 초기화.
        hpPresenter.SetModel(model);
        hpPresenter.SetView(hpView);
        hpPresenter.Initialization();

        expPresenter.SetModel(model);
        expPresenter.SetView(expView);
        expPresenter.Initialization();

        iconPresenter.SetModel(model);
        iconPresenter.SetView(iconView);
        iconPresenter.Initialization();

        levelPresenter.SetModel(model);
        levelPresenter.SetView(levelView);
        levelPresenter.Initialization();
    }
}
