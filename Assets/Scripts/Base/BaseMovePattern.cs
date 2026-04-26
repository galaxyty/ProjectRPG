using UnityEngine;

public abstract class BaseMovePattern : MonoBehaviour
{
    [SerializeField]
    protected float _moveSpeed;

    public abstract void Move(Transform transform, Transform target);
}
