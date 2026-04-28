public class MoveState : IState
{
    private BaseCharacter _character;

    public MoveState(BaseCharacter character)
    {
        _character = character;
    }

    public void UpdateState()
    {
        if (_character.Target == null)
        {
            return;
        }

        float dir = _character.transform.position.DirectionX(_character.Target.transform.position);

        _character.SpriteRenderer.flipX = dir < 0 ? true : false;        
        _character.Animator?.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Enums.eSTATE.Move);

        // 타겟으로 향해 이동.
        _character.MoveStrategy?.ExecuteMove(_character.transform, _character.Target.transform);
    }
}
