using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacter
{
    // 일반 공격 범위.
    private const float _kATTACK_RANGE = 0.2f;

    void Awake()
    {
        AttackStartRange = 0.25f;
        ReverseAttackTime = 200;

        // 상태 변경 조건 룰 추가.
        _decideSystem.AddRule(new AttackRangeDecide(), Consts.eSTATE.Attack);
        _decideSystem.AddRule(new TargetDecide(), Consts.eSTATE.Move);

        // 일반 공격 로직 셋팅.
        AttackBehavior = new RangeAttackBehavior(_kATTACK_RANGE);

        // 상태 초기화.
        _idleState = new(this);
        _moveState = new(this);
        _attackState = new(this);

        // 상태 적용.
        _currentState = _idleState;
        _state = Consts.eSTATE.Idle;        
    }

    protected override void Update()
    {
        base.Update();

        ApplyState();
    }

    // 상태머신 적용.
    private void ApplyState()
    {
        // 애니메이터가 비활성화 되면 상태 갱신 안함.
        if (_animator.enabled == false)
        {
            return;
        }

        // 상태 변경.
        SetState(_decideSystem.DecideState(this));

        // 방향 벡터 갱신.
        UpdateDirection();

        // 상태 호출.
        _currentState.UpdateState();
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

        AttackBehavior.OnBehavior(this).Forget();

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
        Target = null;
        _state = Consts.eSTATE.Idle;
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
