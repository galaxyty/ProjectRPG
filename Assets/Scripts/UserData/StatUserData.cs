using R3;

public class StatUserData : BaseUserData
{
    /// <summary>
    /// 레벨.
    /// </summary>
    public ReactiveProperty<int> Level { get; private set; } = new();

    /// <summary>
    /// 체력.
    /// </summary>
    public ReactiveProperty<int> HP { get; private set; } = new();

    /// <summary>
    /// 경험치.
    /// </summary>
    public ReactiveProperty<int> EXP { get; private set; } = new();

    /// <summary>
    /// 경험치 획득 이벤트.
    /// </summary>
    public Subject<int> OnAddEXP = new();

    public override void Initialization()
    {
        OnAddEXP
            .Subscribe(AddEXP)
            .AddTo(_dispose);
    }

    // 경험치 획득.
    private void AddEXP(int exp)
    {
        
    }
}
