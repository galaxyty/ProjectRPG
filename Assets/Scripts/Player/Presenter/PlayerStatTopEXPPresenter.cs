using R3;
using UnityEngine;

public class PlayerStatTopEXPPresenter : BasePresenter<PlayerStatTopEXPView, PlayerStatModel>
{
    private BindableReactiveProperty<int> _displayLevel = new();
    private BindableReactiveProperty<int> _displayEXP = new();

    public PlayerStatTopEXPPresenter(PlayerStatModel model)
    {
        _displayLevel = model.CurrentLevel.ToBindableReactiveProperty();
        _displayEXP = model.CurrentEXP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        // 데이터 바인딩.
        _displayLevel
            .Skip(1)
            .Subscribe(LevelUP);

        _displayEXP
            .Subscribe(SetEXP);
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

        _model.MaxEXP.Value = data.EXP;

        SetEXP();
    }

    // 경험치 갱신.
    private void SetEXP(int exp = 0)
    {
        _view.SetEXP((float)_model.CurrentEXP.CurrentValue / _model.MaxEXP.CurrentValue);
    }
}
