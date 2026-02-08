using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    /// <summary>
    /// 리소스 초기화.
    /// </summary>
    /// <returns></returns>
    public abstract UniTask InitializationAsync();    
}
