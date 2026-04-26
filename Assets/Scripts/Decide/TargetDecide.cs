
// 타겟 여부 상태 반환.
public class TargetDecide : IDecide
{
    public bool Decide(BaseCharacter character)
    {
        character.Target = MonsterManager.Instance.GetNearTarget(character.transform.position);

        return character.Target != null;
    }
}
