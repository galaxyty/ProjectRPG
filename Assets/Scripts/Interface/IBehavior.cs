using Cysharp.Threading.Tasks;

// 상태 로직 인터페이스.
public interface IBehavior
{
    public UniTask OnBehavior(BaseCharacter character);
}
