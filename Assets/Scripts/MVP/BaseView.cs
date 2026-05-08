using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseView<TPresenter> : MonoBehaviour
{
    /// <summary>
    /// 프레젠트.
    /// </summary>
    protected TPresenter _presenter;

    /// <summary>
    /// 리소스 초기화.
    /// </summary>    
    public abstract UniTask InitializationAsync();

    /// <summary>
    /// 프레젠트 셋팅.
    /// </summary>
    public void SetPresenter(TPresenter presenter)
    {
        _presenter = presenter;
    }
}
