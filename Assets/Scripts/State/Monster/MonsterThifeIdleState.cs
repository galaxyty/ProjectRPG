using UnityEngine;

public class MonsterThifeIdleState : BaseMonsterIdleState
{
    private MonsterThief _monster;

    public MonsterThifeIdleState(MonsterThief monster)
    {
        _monster = monster;
    }

    public override void UpdateState()
    {
        Debug.Log("시프 몬스터 대기");
    }
}
