using UnityEngine;

public class PlayerAttackState : IState
{
    private Animator _animator;

    // 타겟.
    private BaseMonster _target = null;

    public PlayerAttackState(Animator animator)
    {
        _animator = animator;
    }

    // 타겟 지정.
    public void SetTarget(BaseMonster target)
    {
        _target = target;
    }

    public void UpdateState()
    {
        Debug.Log("플레이어 기본 공격");

        if (_target == null)
        {
            return;
        }

        _animator.SetInteger(Consts.kANIMATOR_KEY_STATE, 2);
    }

    public void OnHit()
    {
        Debug.Log("타격 이벤트");

        if (_target == null)
        {
            return;
        }

        _target.TakeDamage(10);
    }
}
