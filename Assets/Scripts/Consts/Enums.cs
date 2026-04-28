/// <summary>
/// 공용적으로 쓰는 Enum 클래스.
/// </summary>
public static class Enums
{
    // 상태.
    public enum eSTATE
    {
        Idle = 0,
        Move,
        Attack
    }

    public enum MonsterType
    {
        Boss,       // 보스.
        Elite,      // 엘리트.
        Normal      // 일반몹.
    }
}
