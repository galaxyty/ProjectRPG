using Cysharp.Threading.Tasks;

// 공격 스트래티지 인터페이스.
public interface IAttackStrategy
{
    /// <summary>
    /// 공격 로직 실행.
    /// </summary>
    public UniTask ExecuteAttack(BaseCharacter character);
}