using UnityEngine;

public class MonsterThief : BaseMonster
{
    public override void Initialization()
    {
        Debug.Log("도적 몬스터 초기화");

        _type = Enums.MonsterType.Normal;
        SetHP(30);

        transform.position = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-1.0f, 1.0f), 0);
    }

    public override void OnDie()
    {
        Debug.Log("도적 몬스터 사망");

        MonsterManager.Instance.Die(Enums.MonsterType.Normal, this);
    }
}
