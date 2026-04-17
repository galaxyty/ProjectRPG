using R3;
using UnityEngine;

public class PlayerStatTopEXPPresenter : BasePresenter<PlayerStatTopEXPView, PlayerStatModel>
{
    private BindableReactiveProperty<int> _displayEXP = new();

    public PlayerStatTopEXPPresenter(PlayerStatModel model)
    {
        _displayEXP = model.CurrentEXP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        // 데이터 바인딩.
        _displayEXP
            .Subscribe(SetEXP);
    }

    protected override void OnBindModel()
    {
        
    }

    // 경험치 갱신.
    private void SetEXP(int exp)
    {
        _view.SetEXP((float)exp / _model.MaxHP.CurrentValue);
    }
}
