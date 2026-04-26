using UnityEngine;

public class PlayerIdleState : IState
{    
    private PlayerController _controller;

    public PlayerIdleState(PlayerController controller)
    {
        _controller = controller;
    }

    public void UpdateState()
    {
        Debug.Log("플레이어 대기중");

        _controller.Animator.SetInteger(Consts.kANIMATOR_KEY_STATE, (int)Consts.eSTATE.Idle);
    }
}
