using R3;
using UnityEngine;

public class PlayerHPPresenter : BasePresenter<PlayerHPView, PlayerStatModel>
{
    private BindableReactiveProperty<int> _displayLevel = new();
    private BindableReactiveProperty<int> _displayHP = new();

    public PlayerHPPresenter(PlayerStatModel model)
    {
        _displayLevel = model.CurrentLevel.ToBindableReactiveProperty();
        _displayHP = model.CurrentHP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        // 데이터 바인딩.
        _displayLevel
            .Skip(1)
            .Subscribe(LevelUP);

        _displayHP
            .Subscribe(SetHP);
    }

    protected override void OnBindModel()
    {
    }

    // 레벨업.
    private void LevelUP(int level)
    {
        var data = TableManager.Instance.StatTableDatas.Find(data => data.LEVEL == level);

        if (data == null)
        {
            return;
        }

        _model.CurrentHP.Value = data.HP;
        _model.MaxHP.Value = data.HP;

        SetHP(_model.CurrentHP.CurrentValue);
    }

    // HP 갱신.
    private void SetHP(int hp)
    {
        _view.SetHP((float)hp / _model.MaxHP.CurrentValue);
    }
}
