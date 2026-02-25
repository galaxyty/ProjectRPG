using UnityEngine;

public class PlayerIdleState : IState
{    
    private Animator _animator;

    public PlayerIdleState(Animator animator)
    {
        _animator = animator;
    }

    public void UpdateState()
    {
        Debug.Log("플레이어 대기중");

        _animator.SetInteger(Consts.kANIMATOR_KEY_STATE, 0);
    }
}
