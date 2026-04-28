using Cysharp.Threading.Tasks;
using UnityEngine;

// 근접 범위 공격.
public class MeleeAOEAttack : IAttackStrategy
{
    private readonly float _kATTACK_RANGE;

    public MeleeAOEAttack(float attackRange)
    {
        _kATTACK_RANGE = attackRange;
    }

    public async UniTask ExecuteAttack(BaseCharacter character)
    {
        if (character.Target == null)
        {
            return;
        }

        // 주변 적을 공격.
        var hits = Physics2D.OverlapCircleAll(character.transform.position + (Vector3)character.CurrentDirection * character.AttackStartRange, _kATTACK_RANGE);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IHealth>(out var monster))
            {
                monster.TakeDamage(10);
            }
        }

        character.Animator.enabled = false;

        // 역경직 딜레이.
        await UniTask.Delay(character.ReverseAttackTime);

        character.Animator.enabled = true;
    }
}
