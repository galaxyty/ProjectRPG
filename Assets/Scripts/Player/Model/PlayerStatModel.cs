using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class PlayerStatModel : BaseModel
{
    // 레벨.
    private ReactiveProperty<int> _currentLevel = new();

    // 현재 체력.
    private ReactiveProperty<int> _currentHP = new();

    // 최대 체력.
    private ReactiveProperty<int> _maxHP = new();

    // 읽기 전용 변수들.
    public ReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
    public ReadOnlyReactiveProperty<int> CurrentHP => _currentHP;
    public ReadOnlyReactiveProperty<int> MaxHP => _maxHP;

    public PlayerStatModel(StatData statData, StatTableData tableData)
    {
        _currentHP = statData.HP;
        _maxHP.Value = tableData.HP;
    }

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
