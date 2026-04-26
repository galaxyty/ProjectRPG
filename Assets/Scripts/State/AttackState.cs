
public class AttackState : IState
{
    private BaseCharacter _character;

    public AttackState(BaseCharacter character)
    {
        _character = character;
    }

    public void UpdateState()
    {
        _character.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Consts.eSTATE.Attack);
    }
}
