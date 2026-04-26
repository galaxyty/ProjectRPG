using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;

public class MonsterThief : BaseMonster
{
    private void Awake()
    {
        /*_idleState = new MonsterThifeIdleState(this);
        _moveState = new MonsterThifeMoveState(this);*/

        // 상태 적용.
        _currentState = _idleState;
        _state = Consts.eSTATE.Idle;

        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                ApplyState();
            })
            .AddTo(this);
    }

    // 상태머신 적용.
    private void ApplyState()
    {
        _state = Consts.eSTATE.Move;

        //_currentState.UpdateState();
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
