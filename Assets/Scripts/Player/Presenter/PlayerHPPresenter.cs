using R3;
using UnityEngine;

public class PlayerHPPresenter : BasePresenter<PlayerHPView, PlayerStatModel>
{
    public BindableReactiveProperty<int> DisplayHP = new();

    public PlayerHPPresenter(PlayerStatModel model)
    {
        DisplayHP = model.CurrentHP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        // 이벤트 구독.
        DisplayHP
            .Subscribe(hp =>
            {
                _view.SetHP((float)hp / _model.MaxHP.CurrentValue);
            });
    }

    protected override void OnBindModel()
    {
    }
}
