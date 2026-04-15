using R3;

public class PlayerHPPresenter : BasePresenter<PlayerHPView, PlayerHPModel>
{
    public BindableReactiveProperty<int> DisplayHP = new();

    public PlayerHPPresenter(PlayerHPModel model)
    {
        DisplayHP = model.CurrentHP.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        
    }

    protected override void OnBindModel()
    {
        
    }
}
