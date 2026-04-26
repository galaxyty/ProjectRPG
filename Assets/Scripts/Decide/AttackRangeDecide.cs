using UnityEngine;

// 공격 가능 상태 여부 반환.
public class AttackRangeDecide : IDecide
{
    public bool Decide(BaseCharacter character)
    {
        // null 체크.
        if (character.Target == null)
        {
            return false;
        }

        // 적 추적.
        if (Vector3.Distance(character.transform.position, character.Target.transform.position) <= character.AttackStartRange)
        {
            // 범위 안에 들었다면 공격.
            return true;
        }

        return false;
    }
}
