using UnityEngine;

// 타겟이 있고 범위 안에 있는지 검색.
public class TargetInOfRange : IDecide
{
    /// <summary>
    /// 타겟이 존재하고 범위 안에 들었다면 true (주로 기본 공격 상태 반환용).
    /// </summary>
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
