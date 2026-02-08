using Cysharp.Threading.Tasks;

public abstract class BaseModel
{
    /// <summary>
    /// 데이터 초기화.
    /// </summary>    
    public abstract UniTask InitializationAsync();
}
