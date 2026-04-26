using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, IHealth
{
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    [Header("이동 패턴")]
    protected BaseMovePattern _movePattern;

    // 이동중인 방향 벡터.
    [Header("이동중인 방향 벡터")]
    [SerializeField]
    protected Vector2 _currentDirection = Vector2.zero;

    // 공격 로직 인터페이스.
    public IBehavior AttackBehavior { get; protected set; }

    // 상태 변경 판단 시스템.
    protected DecideSystem _decideSystem = new();

    // 기본 공용 상태 패턴.
    protected IdleState _idleState;
    protected MoveState _moveState;
    protected AttackState _attackState;

    // 현재 진행 중인 상태 패턴.
    protected IState _currentState;

    // 현재 상태.
    protected Consts.eSTATE _state;

    // 타겟.
    public BaseMonster Target;

    // 타겟 공격 시작 범위.
    public float AttackStartRange { get; protected set; }

    // 기본 공격 역경직 시간 (1000 = 1초 단위).
    public int ReverseAttackTime { get; protected set; }

    public Vector2 CurrentDirection
    {
        get { return _currentDirection; }
        private set { }
    }

    /// <summary>
    /// 체력.
    /// </summary>
    public int Hp { get; private set; }

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

    public BaseMovePattern MovePattern
    {
        get { return _movePattern; }
        private set { }
    }

    protected void Update()
    {
        // 레이어 Order.
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    /// <summary>
    /// 체력 셋팅
    /// </summary>    
    public void SetHP(int hp)
    {
        Hp = hp;
    }

    /// <summary>
    /// 데미지 받음 (피격).
    /// </summary>    
    public void TakeDamage(int damage)
    {
        Debug.Log($"받은 데미지 : {damage}");

        Hp -= damage;

        OnHit();

        if (Hp <= 0)
        {
            OnDie();
        }
    }

    // 상태머신 갱신.
    protected void SetState(Consts.eSTATE state)
    {
        _currentState = _state switch
        {
            Consts.eSTATE.Idle => _idleState,
            Consts.eSTATE.Move => _moveState,
            Consts.eSTATE.Attack => _attackState,
            _ => _idleState
        };

        _state = state;
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
    /// 피격 받을 시 호출.
    /// </summary>
    public abstract UniTask OnHit();

    /// <summary>
    /// 사망.
    /// </summary>
    public abstract void OnDie();
}
