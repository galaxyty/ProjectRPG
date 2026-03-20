using Cysharp.Threading.Tasks;
using R3;
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
    private ReactiveProperty<eSTATE> _state = new();

    // 타겟.
    private ReactiveProperty<BaseMonster> _target = new();

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Animator _animator;

    // 이동중인 방향 벡터.
    [Header("이동중인 방향 벡터")]
    [SerializeField]
    private Vector2 _currentDirection = Vector2.zero;

    // 현재 진행 중인 상태.
    private IState _currentState;

    // 플레이어 가지고 있는 상태.
    private PlayerIdleState _idleState;
    private PlayerMoveState _moveState;
    private PlayerAttackState _attackState;

    // 타겟 공격 시작 범위.
    private const float _kATTACK_START_RANGE = 0.4f;

    // 일반 공격 범위.
    private const float _kATTACK_RANGE = 0.2f;

    // 기본 공격 역경직 시간 (1000 = 1초 단위).
    private const int _kREVERSE_ATTACK_TIME = 200;

    // 프로퍼티.
    public SpriteRenderer SpriteRenderer 
    {
        get { return _spriteRenderer; }
        private set { }
    }

    public Animator Animator
    {
        get { return _animator; }
        private set { }
    }

    public int kREVERSE_ATTACK_TIME
    {
        get { return _kREVERSE_ATTACK_TIME; }
        private set { }
    }

    public float kATTACK_START_RANGE
    {
        get { return _kATTACK_START_RANGE;}
        private set { }
    }

    public float kATTACK_RANGE
    {
        get { return _kATTACK_RANGE; }
        private set { }
    }

    public Vector2 CurrentDirection
    {
        get { return _currentDirection; }
        private set { }
    }

    void Awake()
    {
        // 상태 초기화.
        _idleState = new(this);
        _moveState = new(this);
        _attackState = new(this);

        // 상태 적용.
        _currentState = _idleState;
        _state.Value = eSTATE.Idle;
    }

    void Start()
    {
        // 이벤트 구독.
        _target
            .Subscribe(SetTarget)
            .AddTo(this);

        _state
            .Subscribe(SetState)
            .AddTo(this);
    }

    void Update()
    {
        ApplyState();

        // 레이어 Order.
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    // 상태머신 적용.
    private void ApplyState()
    {
        // 애니메이터가 비활성화 되면 상태 갱신 안함.
        if (_animator.enabled == false)
        {
            return;
        }

        // 가까운 타겟 가져옴.
        if (_state.Value == eSTATE.Idle)
        {
            // 타겟 작동에 안전한 호출을 위해 null로 만들어줌.
            _target.Value = null;
            _target.Value = MonsterManager.Instance.GetNearTarget(transform.position);
        }        

        // null 체크.
        if (_target.Value != null)
        {
            // 적 추적.
            if (Vector3.Distance(transform.position, _target.Value.transform.position) <= _kATTACK_START_RANGE)
            {
                // 범위 안에 들었다면 공격.
                _state.Value = eSTATE.Attack;
            }
        }        

        // 상태 호출.
        _currentState.UpdateState();
    }

    // 타겟 여부에 따른 idle/move 변환.
    private void SetTarget(BaseMonster target)
    {
        Debug.Log("플레이어가 타겟을 포착");

        if (target == null)
        {
            // 타겟이 없으면 대기 상태.
            _state.Value = eSTATE.Idle;
            return;
        }

        _moveState.SetTarget(_target.Value);
        _attackState.SetTarget(_target.Value);

        // 방향 벡터.
        _currentDirection = (target.transform.position - transform.position).normalized;

        // 적을 추적할 수 있게 이동 상태.
        _state.Value = eSTATE.Move;
    }

    // 상태머신 갱신.
    private void SetState(eSTATE state)
    {
        Debug.Log("플레이어 상태머신 갱신");

        _currentState = _state.Value switch
        {
            eSTATE.Idle => _idleState,
            eSTATE.Move => _moveState,
            eSTATE.Attack => _attackState,
            _ => _idleState
        };
    }

    /// <summary>
    /// 애니메이터 콜백 함수.
    /// </summary>
    public void OnHit()
    {
        if (_attackState == null)
        {
            return;
        }

        _attackState.OnHit().Forget();
    }

    /// <summary>
    /// 애니메이터 기본 공격 종료 함수.
    /// </summary>
    public void OnAttackEnd()
    {
        _state.Value = eSTATE.Idle;
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
        Gizmos.DrawWireSphere(transform.position + (Vector3)_currentDirection * _kATTACK_START_RANGE, _kATTACK_RANGE);
    }
}
