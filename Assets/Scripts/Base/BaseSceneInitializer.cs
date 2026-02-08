using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseSceneInitializer : MonoBehaviour
{
    /// <summary>
    /// æ¿ √ ±‚»≠.
    /// </summary>    
    public abstract UniTask InitializationAsync();
}
