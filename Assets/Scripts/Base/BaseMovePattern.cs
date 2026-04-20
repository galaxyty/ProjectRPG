using UnityEngine;

public abstract class BaseMovePattern : MonoBehaviour
{
    public abstract void Move(Transform transform, Transform target);
}
