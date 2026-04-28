using Cysharp.Threading.Tasks;

public interface IFactory
{
    /// <summary>
    /// 팩토리 생성.
    /// </summary>    
    public UniTask CreateAsync();
}
