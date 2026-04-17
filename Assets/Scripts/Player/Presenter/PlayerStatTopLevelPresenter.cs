using R3;
using UnityEngine;

public class PlayerStatTopLevelPresenter : BasePresenter<PlayerStatTopLevelView, PlayerStatModel>
{
    // 레벨.
    private BindableReactiveProperty<int> _displayLevel = new();

    // 레벨 텍스트.
    private const string kLEVEL_TEXT = "Lv. {0}";

    public PlayerStatTopLevelPresenter(PlayerStatModel model)
    {
        _displayLevel = model.CurrentLevel.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        // 데이터 바인딩.
        _displayLevel
            .Subscribe(SetLevel);
    }

    protected override void OnBindModel()
    {
        
    }

    // 레벨 UI 변경.
    private void SetLevel(int level)
    {
        _view.SetLevel(string.Format(kLEVEL_TEXT, level));
    }
}
