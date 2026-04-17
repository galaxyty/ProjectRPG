using R3;

public class StatUserData
{
    /// <summary>
    /// 레벨.
    /// </summary>
    public ReactiveProperty<int> Level = new();

    /// <summary>
    /// 체력.
    /// </summary>
    public ReactiveProperty<int> HP = new();

    /// <summary>
    /// 경험치.
    /// </summary>
    public ReactiveProperty<int> EXP = new();
}
