using UnityEngine;
using R3;

public class StageUserData : BaseUserData
{
    /// <summary>
    /// 스테이지 라운드.
    /// </summary>
    public ReactiveProperty<int> StageLevel { get; private set; } = new();

    public override void Initialization()
    {
        
    }
}
