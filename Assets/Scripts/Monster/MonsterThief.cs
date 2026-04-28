using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterThief : BaseMonster
{
    protected override void Awake()
    {
        _moveSpeed = 0.2f;
        AttackStartRange = 0.25f;
        ReverseAttackTime = 200;

        // 상태 변경 조건 룰 추가.
        _decideSystem.AddRule(new TargetOutOfRangeDecide(), Enums.eSTATE.Move);
        _decideSystem.AddRule(new TargetInOfRangeDecide(), Enums.eSTATE.Attack);

        // 움직임 로직 셋팅.
        MoveStrategy = new StraightMove(_moveSpeed);

        // 일반 공격 로직 셋팅.
        AttackStrategy = new MeleeSingleAttack();

        base.Awake();
    }

    protected override void Update()
    {
        // 타겟 찾기.
        Target = GameObject.Find("Player").GetComponent<BaseCharacter>();

        base.Update();
    }

    public override void Initialization()
    {
        Debug.Log("도적 몬스터 초기화");

        Type = Enums.MonsterType.Normal;
        _spriteRenderer.color = new Color(1, 1, 1);

        _currentHP.Value = 30;
    }

    public override void OnDie()
    {
        Debug.Log("도적 몬스터 사망");
        
        MonsterManager.Instance.Die(this, Consts.kPATH_MONSTER_THIEF);

        DataManager.Instance.StatUserData.OnAddEXP.OnNext(100);
    }

    public override async UniTask OnHit()
    {
        _spriteRenderer.color = new Color(1, 0, 0);

        await UniTask.Delay(200);

        _spriteRenderer.color = new Color(1, 1, 1);
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
}
