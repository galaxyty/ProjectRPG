using Cysharp.Threading.Tasks;
using UnityEngine;

public class ResourceManager : BaseObjectSingleton<ResourceManager>
{
    /// <summary>
    /// T타입 리소스 로드.
    /// </summary>    
    public async UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object
    {
        T resource = await Resources.LoadAsync(key) as T;

        return resource;
    }

    /// <summary>
    /// 리소스 다운로드.
    /// </summary>    
    public async UniTask Download()
    {        
        if (IsDownload() == true)
        {
            // 다운로드 완료 된 상태.
            Debug.Log("리소스 다운로드 완료!");
        }
        else
        {
            // 다운로드 시작.
            Debug.Log("리소스 다운로드 시작...");
            await UniTask.Delay(1000);
            Debug.Log("리소스 다운로드 완료!");
        }                
    }

    // 리소스 다운로드가 완료 된 상태인지?
    private bool IsDownload()
    {
        // 임시 반환.
        return false;
    }
}
