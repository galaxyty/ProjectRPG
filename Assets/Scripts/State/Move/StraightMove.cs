using UnityEngine;

public class StraightMove : BaseMovePattern
{
    public override void Move(Transform transform, Transform target)
    {
        transform.position = Vector3.MoveTowards(
        transform.transform.position,
        target.transform.position,
        1.0f * Time.deltaTime
        );
    }
}
