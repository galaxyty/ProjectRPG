using UnityEngine;

public class PlayerMoveState : IState
{    
    private PlayerController _controller;

    // 타겟.
    private BaseMonster _target = null;

    public PlayerMoveState(PlayerController controller)
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
        Debug.Log("플레이어 움직임");
        
        if (_target == null)
        {
            return;
        }

        float dir = _controller.transform.position.DirectionX(_target.transform.position);

        _controller.SpriteRenderer.flipX = dir < 0 ? true : false;

        _controller.transform.position = Vector3.MoveTowards(
        _controller.transform.position,
        _target.transform.position,
        1.0f * Time.deltaTime
        );

        _controller.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, 1);
    }
}
