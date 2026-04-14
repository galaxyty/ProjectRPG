using R3;
using UnityEngine;

public class PlayerHPPresenter : BasePresenter<PlayerHPView, PlayerHPModel>
{
    public BindableReactiveProperty<int> DisplayHP = new();

    public PlayerHPPresenter(PlayerHPModel model)
    {
        DisplayHP = model.HP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        
    }

    protected override void OnBindModel()
    {
        
    }
}
