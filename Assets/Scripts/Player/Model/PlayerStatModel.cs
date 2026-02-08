using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class PlayerStatModel : BaseModel
{
    public ReactiveProperty<int> Index = new();

    public PlayerStatModel(TestData data)
    {
        Index = new(data.INDEX);
    }

    public override UniTask InitializationAsync()
    {
        Debug.Log($"PlayerStatModel 초기화 시작");

        return UniTask.CompletedTask;
    } 
}
