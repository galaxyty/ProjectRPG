using Cysharp.Threading.Tasks;
using UnityEngine;

public class MonsterThief : BaseMonster
{
    public override void Initialization()
    {
        Debug.Log("도적 몬스터 초기화");

        _type = Enums.MonsterType.Normal;
        _spriteRenderer.color = new Color(1, 1, 1);
        SetHP(30);

        transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    public override void OnDie()
    {
        Debug.Log("도적 몬스터 사망");

        MonsterManager.Instance.Die(this, Consts.kPATH_MONSTER_THIEF);
    }

    public override async UniTask OnHit()
    {
        _spriteRenderer.color = new Color(1, 0, 0);

        await UniTask.Delay(200);

        _spriteRenderer.color = new Color(1, 1, 1);
    }
}
