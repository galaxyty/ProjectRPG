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

    public override void Initialization()
    {
        
    }

    /// <summary>
    /// 1레벨 데이터로 셋팅.
    /// </summary>
    public void InitFirstData()
    {
        var data = TableManager.Instance.StatTableDatas[0];

        if (data == null)
        {
            return;
        }

        Level.Value = data.LEVEL;
        HP.Value = data.HP;
    }
}
