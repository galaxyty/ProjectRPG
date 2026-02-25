// 체력 인터페이스.
public interface IHealth
{
    // 체력.
    public int Hp { get; }

    /// <summary>
    /// 피격 (데미지 받음).
    /// </summary>    
    public void TakeDamage(int damage);

    /// <summary>
    /// 체력 셋팅.
    /// </summary>    
    public void SetHP(int hp);

    /// <summary>
    /// 사망 이벤트.
    /// </summary>
    public void OnDie();
}
