using UnityEngine;

public interface IMoveStrategy
{
    public void ExecuteMove(Transform transform, Transform target);
}
