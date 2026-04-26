using UnityEngine;

// 타겟 여부 상태 반환.
public class TargetDecide : IDecide
{
    /// <summary>
    /// 타겟이 있으면 이동 상태.
    /// </summary>    
    public bool Decide(BaseCharacter character)
    {
        character.Target = MonsterManager.Instance.GetNearTarget(character.transform.position);

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
