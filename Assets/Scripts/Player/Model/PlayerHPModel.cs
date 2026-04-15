using Cysharp.Threading.Tasks;
using R3;

public class PlayerHPModel : BaseModel
{
    // 현재 체력.
    private ReactiveProperty<int> _currentHP = new();

    // 최대 체력.
    private ReactiveProperty<int> _maxHP = new();

    // 읽기 전용 변수들.
    public ReadOnlyReactiveProperty<int> CurrentHP => _currentHP;

    public ReadOnlyReactiveProperty<int> MaxHP => _maxHP;

    public PlayerHPModel(StatData statData)
    {
        _currentHP = statData.HP;
    }

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }
}
