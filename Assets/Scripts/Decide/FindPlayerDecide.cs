using UnityEngine;

public class FindPlayerDecide : IDecide
{
    public bool Decide(BaseCharacter character)
    {
        character.Target = GameObject.Find("Player").GetComponent<BaseCharacter>();

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
