using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum eSTATE
    {
        Idle = 0,
        Move,
        Attack
    }

    // 플레이어 상태.
    private eSTATE _state = eSTATE.Idle;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Animator _animator;

    // 현재 진행 중인 상태.
    private IState _currentState;

    // 플레이어 가지고 있는 상태.
    private PlayerIdleState _idleState;
    private PlayerMoveState _moveState;
    private PlayerAttackState _attackState;

    // 타겟 공격 시작 범위.
    private const float kATTACK_RANGE = 0.5f;

    private void Awake()
    {
        // 상태 초기화.
        _idleState = new(_animator);
        _moveState = new(transform, _spriteRenderer, _animator);
        _attackState = new(_animator);

        // 상태 적용.
        _currentState = _idleState;
        _state = eSTATE.Idle;
    }

    void Update()
    {
        CheckState();
        SetState();
    }

    // 상태 조건 확인.
    private void CheckState()
    {
        var target = MonsterManager.Instance.GetNearTarget(transform.position);
        _moveState.SetTarget(target);
        _attackState.SetTarget(target);

        // 상태 조건 확인.
        if (target == null)
        {
            // 적 발견 못했으면 기본 상태.
            _state = eSTATE.Idle;
        }
        else
        {
            // 적 추적.
            if (Vector3.Distance(transform.position, target.transform.position) <= kATTACK_RANGE)
            {                
                _state = eSTATE.Attack;
            }
            else
            {
                _state = eSTATE.Move;
            }                
        }
    }

    // 상태 주입.
    private void SetState()
    {
        // 상태 주입.
        _currentState = _state switch
        {
            eSTATE.Idle => _idleState,
            eSTATE.Move => _moveState,
            eSTATE.Attack => _attackState,
            _ => _idleState
        };

        _currentState.UpdateState();
    }

    public void OnHit()
    {
        if (_attackState == null)
        {
            return;
        }

        _attackState.OnHit();
    }

    // 키 입력.
    private void OnMove(InputValue value)
    {
        // 이동 좌표.
        Vector2 input = value.Get<Vector2>();
    }
}
