using Cysharp.Threading.Tasks;
using UnityEngine;

// 단일 타겟 공격.
public class MeleeSingleAttack : IAttackStrategy
{    
    public async UniTask ExecuteAttack(BaseCharacter character)
    {
        if (character.Target == null)
        {
            return;
        }

        // 공격.
        character.Target.TakeDamage(10);

        character.Animator.enabled = false;

        // 역경직 딜레이.
        await UniTask.Delay(character.ReverseAttackTime);

        character.Animator.enabled = true;
    }
}
