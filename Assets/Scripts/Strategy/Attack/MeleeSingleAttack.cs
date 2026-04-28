using Cysharp.Threading.Tasks;
using UnityEngine;

// 단일 타겟 공격.
public class MeleeSingleAttack : IAttackStrategy
{
    public UniTask ExecuteAttack(BaseCharacter character)
    {
        return UniTask.CompletedTask;
    }
}
