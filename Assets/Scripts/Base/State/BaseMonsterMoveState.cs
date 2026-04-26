public abstract class BaseMonsterMoveState : IState
{
    // Å¸°Ù.
    protected PlayerController _target = null;

    public abstract void UpdateState();
}
