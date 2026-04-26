using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterThief : BaseMonster
{
    protected override void Awake()
    {
        AttackStartRange = 0.25f;
        ReverseAttackTime = 200;

        // 상태 변경 조건 룰 추가.
        _decideSystem.AddRule(new FindPlayerDecide(), Consts.eSTATE.Move);
        //_decideSystem.AddRule(new AttackRangeDecide(), Consts.eSTATE.Attack);

        // 일반 공격 로직 셋팅.
        //AttackBehavior = new RangeAttackBehavior(_kATTACK_RANGE);

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        ApplyState();
    }

    public override void Initialization()
    {
        Debug.Log("도적 몬스터 초기화");

        Type = Enums.MonsterType.Normal;
        _spriteRenderer.color = new Color(1, 1, 1);
        SetHP(30);
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
}
