
// 몬스터 기본 클래스
public abstract class BaseMonster : BaseCharacter
{
    protected int _index;

    /// <summary>
    /// 몬스터 초기화.
    /// </summary>
    public abstract void Initialization(int index);

    /// <summary>
    /// 몬스터 타입.
    /// </summary>
    public Enums.MonsterType Type { get; protected set; }
}