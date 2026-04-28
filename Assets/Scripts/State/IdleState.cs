
public class IdleState : IState
{
    private BaseCharacter _character;

    public IdleState(BaseCharacter character)
    {
        _character = character;
    }

    public void UpdateState()
    {
        _character.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Enums.eSTATE.Idle);
    }
}
