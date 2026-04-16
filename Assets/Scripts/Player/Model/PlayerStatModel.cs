using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class PlayerStatModel : BaseModel
{
    // 레벨.
    public ReactiveProperty<int> CurrentLevel = new();

    // 현재 체력.
    public ReactiveProperty<int> CurrentHP = new();

    // 최대 체력.
    public ReactiveProperty<int> MaxHP = new();

    public PlayerStatModel(StatUserData userData, StatTableData tableData)
    {
        CurrentLevel = userData.Level;
        CurrentHP = userData.HP;
        MaxHP.Value = tableData.HP;
    }

    public override UniTask InitializationAsync()
    {
        // 이벤트 구독.
        CurrentLevel
            .Skip(1)
            .Subscribe(level =>
            {
                Debug.Log($"플레이어 레벨업 : {level}");
            });

        return UniTask.CompletedTask;
    }
}
