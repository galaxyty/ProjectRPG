using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : BaseObjectSingleton<ResourceManager>
{
    private bool _isDownload = false;

    /// <summary>
    /// 리소스 폴더에서 T타입 리소스 로드.
    /// </summary>
    public async UniTask<T> LoadAsyncToResource<T>(string key) where T : UnityEngine.Object
    {
        T resource = await Resources.LoadAsync(key) as T;

        return resource;
    }

    /// <summary>
    /// 어드레서블 에셋에서 T타입 리소스 로드.
    /// </summary>
    public async UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object
    {
        T resource = await Addressables.LoadAssetAsync<T>(key);

        return resource;
    }

    /// <summary>
    /// 리소스 다운로드.
    /// </summary>    
    public async UniTask Download()
    {        
        if (_isDownload == true)
        {
            // 다운로드 완료 된 상태.
            Debug.Log("리소스 다운로드가 이미 완료 된 상태");
        }
        else
        {
            // 다운로드 시작.
            Debug.Log("리소스 다운로드 시작...");

            var handle = Addressables.DownloadDependenciesAsync("default");

            await handle.ToUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {                
                Debug.Log("리소스 다운로드 완료!");
            }
            else
            {
                Debug.LogError("리소스 다운로드 실패...");
            }                

            _isDownload = true;
        }
    }
}
