using Cysharp.Threading.Tasks;
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

        _controller.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)PlayerController.eSTATE.Attack);
    }

    public async UniTask OnHit()
    {
        Debug.Log("타격 이벤트");

        if (_target == null)
        {
            return;
        }

        // 주변 적을 공격.
        var hits = Physics2D.OverlapCircleAll(_controller.transform.position + (Vector3)_controller.CurrentDirection * _controller.kATTACK_START_RANGE, _controller.kATTACK_RANGE);

        foreach (var hit in hits)
        {            
            if (hit.TryGetComponent<IHealth>(out var monster))
            {
                monster.TakeDamage(10);
            }
        }

        _controller.Animator.enabled = false;

        // 역경직 딜레이.
        await UniTask.Delay(_controller.kREVERSE_ATTACK_TIME);

        _controller.Animator.enabled = true;
    }
}
