
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

        // 타겟으로 향해 이동.
        _character.MovePattern?.Move(_character.transform, _character.Target.transform);
        _character.Animator?.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Consts.eSTATE.Move);
    }
}
