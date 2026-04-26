using UnityEngine;

public class MonsterThifeMoveState : BaseMonsterMoveState
{
    private MonsterThief _monster;

    public MonsterThifeMoveState(MonsterThief monster)
    {
        _monster = monster;
        _target = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public override void UpdateState()
    {
        Debug.Log("시프 몬스터 움직임");

        if (_target == null)
        {
            return;
        }

        float dir = _monster.transform.position.DirectionX(_target.transform.position);

        _monster.SpriteRenderer.flipX = dir < 0 ? true : false;

        // 움직임.
        _monster.MovePattern?.Move(_monster.transform, _target.transform);        

        _monster.Animator?.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Consts.eSTATE.Move);
    }
}
