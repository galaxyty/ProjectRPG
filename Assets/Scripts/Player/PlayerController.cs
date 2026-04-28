using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

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
        _decideSystem.AddRule(new TargetOutOfRange(), Enums.eSTATE.Move);
        _decideSystem.AddRule(new TargetInOfRange(), Enums.eSTATE.Attack);

        // 움직임 로직 셋팅.
        MoveStrategy = new StraightMove(_moveSpeed);

        // 일반 공격 로직 셋팅.
        AttackStrategy = new MeleeAOEAttack(_kATTACK_RANGE);

        _currentHP = DataManager.Instance.StatUserData.HP;

        base.Awake();
    }

    protected override void Update()
    {
        // 타겟 찾기.
        Target = MonsterManager.Instance.GetNearTarget(transform.position);

        base.Update();
    }
    
    public override async UniTask OnHit()
    {
        _spriteRenderer.color = new Color(1, 0, 0);

        await UniTask.Delay(200);

        _spriteRenderer.color = new Color(1, 1, 1);
    }

    public override void OnDie()
    {
    }

    /// <summary>
    /// 애니메이터 콜백 함수.
    /// </summary>
    public void OnAnimationHit()
    {
        if (_attackState == null)
        {
            return;
        }

        AttackStrategy.ExecuteAttack(this).Forget();
    }

    /// <summary>
    /// 애니메이터 기본 공격 종료 함수.
    /// </summary>
    public void OnAnimationAttackEnd()
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
