using UnityEngine;

// 타겟이 존재하고 해당 범위 밖에 있는지 검색.
public class TargetOutOfRange : IDecide
{
    /// <summary>
    /// 타겟이 존재하고 범위 밖에면 true (주로 이동 상태 반환용).
    /// </summary>    
    public bool Decide(BaseCharacter character)
    {
        if (character.Target == null)
        {
            return false;
        }

        if (Vector3.Distance(character.transform.position, character.Target.transform.position) <= character.AttackStartRange)
        {
            // 공격 범위 안에 들면 이동 취소.
            return false;
        }

        return true;
    }
}
