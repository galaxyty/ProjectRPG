using R3;

public abstract class BaseUserData
{
    // R3 구독 해제 Dispose.
    protected CompositeDisposable _dispose = new();

    /// <summary>
    /// 초기화.
    /// </summary>
    public abstract void Initialization();

    /// <summary>
    /// 구독 해제.
    /// </summary>
    public void Dispose()
    {
        _dispose.Dispose();
    }
}
