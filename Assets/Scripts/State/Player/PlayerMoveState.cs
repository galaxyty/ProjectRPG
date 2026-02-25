using UnityEngine;

public class PlayerMoveState : IState
{    
    private SpriteRenderer _spriteRenderer;

    private Transform _transform;

    private Animator _animator;

    // 타겟.
    private BaseMonster _target = null;

    public PlayerMoveState(Transform transform, SpriteRenderer spriteRenderer, Animator animator)
    {
        _transform = transform;
        _spriteRenderer = spriteRenderer;
        _animator = animator;
    }

    // 타겟 지정.
    public void SetTarget(BaseMonster target)
    {
        _target = target;
    }

    public void UpdateState()
    {
        Debug.Log("플레이어 움직임");
        
        if (_target != null)
        {
            float dir = _transform.position.DirectionX(_target.transform.position);

            if (dir < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            _transform.position = Vector3.MoveTowards(
            _transform.position,
            _target.transform.position,
            1.0f * Time.deltaTime
            );            

            _animator.SetInteger(Consts.kANIMATOR_KEY_STATE, 1);
        }
    }
}
