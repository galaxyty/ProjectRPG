using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : BaseObjectSingleton<ResourceManager>
{
    // 퍼센트.
    private ReactiveProperty<float> _percent = new();

    // 다운로드 받을 용량.
    private ReactiveProperty<string> _size = new();

    /// <summary>
    /// 퍼센트.
    /// </summary>
    public ReactiveProperty<float> Percent
    {
        get { return _percent; }
        private set { }
    }

    /// <summary>
    /// 다운로드 받을 용량.
    /// </summary>
    public ReactiveProperty<string> Size
    {
        get { return _size; }
        private set { }
    }

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
    /// 어드레서블 리소스 다운로드 검사.
    /// </summary>    
    public async UniTask CheckDownloadAsync()
    {        
        // 다운로드 시작.
        Debug.Log("리소스 다운로드 시작...");

        // 번들 용량 가져옴.
        var (isDownload, size) = await GetDownloadSizeAsync(Consts.kAD_KEY_TEST);

        Debug.Log($"번들 다운로드 사이즈 : {FormatBytes(size)}");

        _size.Value = FormatBytes(size);

        // 번들이 이미 다운로드 된 상태인지 확인.
        if (isDownload == false)
        {
            // 번들이 이미 다운로드 된 상태.
            _percent.Value = 1;
            var popup = PopupManager.Instance.Show<ResourceDownloadPopup>();

            // 팝업 선택 결과 기다림.
            var isSelect = await popup.Result.Task;

            if (isSelect == true)
            {
                // 확인 버튼 누름.
            }
            else
            {
                // 취소 버튼 누름.
            }
        }
        else
        {
            // 번들 다운로드 팝업.
            var popup = PopupManager.Instance.Show<ResourceDownloadPopup>();

            // 팝업 선택 결과 기다림.
            var isSelect = await popup.Result.Task;
        }
    }

    // 어드레서블 리소스 다운로드.
    private async UniTask DownloadAsync()
    {
        // 번들 다운로드하거나, 새로 받아야 하는 상태.
        var handle = Addressables.DownloadDependenciesAsync(Consts.kAD_KEY_TEST);

        while (!handle.IsDone)
        {
            await UniTask.Yield();

            _percent.Value = handle.GetDownloadStatus().Percent;
        }

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("리소스 다운로드 완료!");
            _percent.Value = handle.GetDownloadStatus().Percent;
        }
        else
        {
            Debug.LogError("리소스 다운로드 실패...");
            _percent.Value = 0;
        }

        Addressables.Release(handle);
    }

    // 번들 다운로드 용량 반환.
    private async UniTask<(bool isDownload, long size)> GetDownloadSizeAsync(string key)
    {
        // 비동기 사이즈 가져온다.
        var handle = Addressables.GetDownloadSizeAsync(key);

        await handle;

        // 번들 용량 변수에 담음.
        var size = handle.Result;

        // 사용한 핸들 메모리 릴리즈.
        Addressables.Release(handle);

        return (size != 0, size);
    }

    // 용량을 Byte 단위로 변환.
    private string FormatBytes(long bytes)
    {
        float KB = 1024f;
        float MB = KB * 1024f;
        float GB = MB * 1024f;

        if (bytes >= GB)
            return $"{bytes / GB:F2} GB";
        else if (bytes >= MB)
            return $"{bytes / MB:F2} MB";
        else if (bytes >= KB)
            return $"{bytes / KB:F2} KB";
        else
            return $"{bytes} B";
    }
}
