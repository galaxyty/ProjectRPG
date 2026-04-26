using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController _controller;

    // 타겟.
    private BaseMonster _target = null;

    public PlayerAttackState(PlayerController controller)
    {
        _controller = controller;
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

        float dir = _controller.transform.position.DirectionX(_target.transform.position);

        _controller.SpriteRenderer.flipX = dir < 0 ? true : false;

        _controller.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Consts.eSTATE.Attack);
    }
}
