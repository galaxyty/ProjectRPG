// Vector3 확정메소드.
using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// X축 기준 비교하여 왼쪽인지, 오른쪽인지 판별 (0보다 크면 오른쪽).
    /// </summary>    
    public static float DirectionX(this Vector3 original, Vector3 target)
    {
        return target.x - original.x;
    }
}
