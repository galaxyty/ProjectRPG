using UnityEngine;

// 직선 이동 로직.
public class StraightMove : IMoveStrategy
{
    private readonly float kMOVE_SPEED;

    public StraightMove(float moveSpeed)
    {
        kMOVE_SPEED = moveSpeed;
    }

    public void ExecuteMove(Transform transform, Transform target)
    {
        /*Vector3 push = Vector3.zero;

        var hits = Physics2D.OverlapCircleAll(transform.transform.position, 0.5f);

        foreach (var h in hits)
        {
            if (h.transform == transform.transform) continue;

            push += (transform.transform.position - h.transform.position);
        }

        var moveDir = (target.transform.position - transform.transform.position).normalized;

        transform.position +=
            (moveDir +
            push.normalized).normalized * 
            _moveSpeed * 
            Time.deltaTime;*/

        var moveDir = (target.transform.position - transform.transform.position).normalized;

        transform.position +=
            moveDir *
            kMOVE_SPEED *
            Time.deltaTime;
    }
}
