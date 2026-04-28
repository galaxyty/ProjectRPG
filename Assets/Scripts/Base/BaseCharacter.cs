using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{    
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    [SerializeField]
    protected Animator _animator;    

    // 이동중인 방향 벡터.
    [Header("이동중인 방향 벡터")]
    [SerializeField]
    protected Vector2 _currentDirection = Vector2.zero;

    // 움직임 로직 인터페이스.
    public IMoveStrategy MoveStrategy;

    // 공격 로직 인터페이스.
    public IAttackStrategy AttackStrategy { get; protected set; }

    // 상태 변경 판단 시스템.
    protected DecideSystem _decideSystem = new();

    // 기본 공용 상태 패턴.
    protected IdleState _idleState;
    protected MoveState _moveState;
    protected AttackState _attackState;

    // 현재 진행 중인 상태 패턴.
    protected IState _currentState;

    // 현재 상태.
    protected Enums.eSTATE _state;

    // 타겟.
    [HideInInspector]
    public BaseCharacter Target;

    // 이동 속도.
    protected float _moveSpeed;

    // 타겟 공격 시작 범위.
    public float AttackStartRange { get; protected set; }

    // 기본 공격 역경직 시간 (1000 = 1초 단위).
    public int ReverseAttackTime { get; protected set; }

    // 현재 체력.
    protected ReactiveProperty<int> _currentHP = new();

    // 현재 경험치.
    protected ReactiveProperty<int> _currentEXP = new();

    // 프로퍼티.
    public Vector2 CurrentDirection
    {
        get { return _currentDirection; }
        private set { }
    }
    
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

    protected virtual void Awake()
    {
        // 상태 초기화.
        _idleState = new(this);
        _moveState = new(this);
        _attackState = new(this);

        // 상태 적용.
        _currentState = _idleState;
        _state = Enums.eSTATE.Idle;
    }

    protected virtual void Update()
    {
        // 레이어 Order.
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);

        ApplyState();
    }    

    // 상태머신 갱신.
    protected void SetState(Enums.eSTATE state)
    {
        _currentState = _state switch
        {
            Enums.eSTATE.Idle => _idleState,
            Enums.eSTATE.Move => _moveState,
            Enums.eSTATE.Attack => _attackState,
            _ => _idleState
        };

        _state = state;
    }

    // 상태머신 적용.
    protected void ApplyState()
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

    // 방향 벡터 갱신.
    protected void UpdateDirection()
    {
        // 방향 벡터.
        if (Target != null)
        {
            _currentDirection = (Target.transform.position - transform.position).normalized;
        }
    }

    /// <summary>
    /// 경험치 획득.
    /// </summary>
    public void AddEXP(int exp)
    {
        _currentEXP.Value += exp;
    }

    /// <summary>
    /// 데미지 받음 (피격).
    /// </summary>    
    public void TakeDamage(int damage)
    {
        Debug.Log($"받은 데미지 : {damage}");

        _currentHP.Value -= damage;

        OnHit();

        if (_currentHP.Value <= 0)
        {
            OnDie();
        }
    }

    /// <summary>
    /// 피격 받을 시 호출.
    /// </summary>
    public abstract UniTask OnHit();

    /// <summary>
    /// 사망.
    /// </summary>
    public abstract void OnDie();
}
