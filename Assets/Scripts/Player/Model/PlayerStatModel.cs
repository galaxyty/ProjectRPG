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

    // 현재 경험치.
    public ReactiveProperty<int> CurrentEXP = new();

    // 현재 레벨 기준 목표 도달 경험치.
    public ReactiveProperty<int> MaxEXP = new();

    public PlayerStatModel(StatUserData userData, StatTableData tableData)
    {
        CurrentLevel = userData.Level;
        CurrentHP = userData.HP;
        CurrentEXP = userData.EXP;

        MaxHP.Value = tableData.HP;
        MaxEXP.Value = tableData.EXP;
    }

    public override UniTask InitializationAsync()
    {
        // 데이터 바인딩.
        CurrentLevel
            .Skip(1)
            .Subscribe(level =>
            {
                Debug.Log($"플레이어 레벨업 : {level}");
            });

        return UniTask.CompletedTask;
    }
}
