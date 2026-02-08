using R3;
using UnityEngine;

public class PlayerStatPresenter : BasePresenter<PlayerStatView, PlayerStatModel>
{
    public BindableReactiveProperty<int> DisplayIndex = new();

    public PlayerStatPresenter(PlayerStatModel model)
    {
        DisplayIndex = model.Index.ToBindableReactiveProperty();
    }

    public override void Initialization()
    {
        Debug.Log("스탯 초기화 호출");

        _view.OnTest
            .Subscribe(_ =>
            {                      
                Debug.Log($"클릭 : {DisplayIndex.Value}");

                _model.Index.Value += 1;
            });
    }

    protected override void OnBindModel()
    {
    }
}
