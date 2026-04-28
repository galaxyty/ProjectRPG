using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacter
{
    // 일반 공격 범위.
    private const float _kATTACK_RANGE = 0.2f;

    protected override void Awake()
    {
        _moveSpeed = 1.0f;
        AttackStartRange = 0.25f;
        ReverseAttackTime = 200;

        // 상태 변경 조건 룰 추가.
        _decideSystem.AddRule(new TargetDecide(), Enums.eSTATE.Move);
        _decideSystem.AddRule(new AttackRangeDecide(), Enums.eSTATE.Attack);

        // 움직임 로직 셋팅.
        MoveStrategy = new StraightMove(_moveSpeed);

        // 일반 공격 로직 셋팅.
        AttackStrategy = new MeleeAOEAttack(_kATTACK_RANGE);

        base.Awake();
    }   

    /// <summary>
    /// 애니메이터 콜백 함수.
    /// </summary>
    public override UniTask OnHit()
    {
        if (_attackState == null)
        {
            return UniTask.CompletedTask;
        }

        AttackStrategy.ExecuteAttack(this).Forget();

        return UniTask.CompletedTask;
    }

    public override void OnDie()
    {
    }

    /// <summary>
    /// 애니메이터 기본 공격 종료 함수.
    /// </summary>
    public void OnAttackEnd()
    {
        _state = Enums.eSTATE.Idle;
        SetState(_state);
    }

    // 키 입력.
    private void OnMove(InputValue value)
    {
        // 이동 좌표.
        Vector2 input = value.Get<Vector2>();
    }

    private void OnDrawGizmos()
    {
        // 일반 공격 범위 시각화.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (Vector3)_currentDirection * AttackStartRange, _kATTACK_RANGE);
    }
}
