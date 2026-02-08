using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : BaseObjectSingleton<SceneLoadManager>
{
    // 로딩 바 클래스.
    [SerializeField]
    private LoadingSceneView _loadingSceneView;

    // 로딩 바 오브젝트.
    private GameObject _loadingObject;

    // 로딩 싱글톤 초기화 여부.
    private bool _isInit = false;

    /// <summary>
    /// 씬 매니저 초기화.
    /// </summary>    
    public async UniTask InitializationAsync()
    {
        // 초기화 여부 확인.
        if (_isInit == true)
        {
            return;
        }

        // 리소스에서 프리팹 로드.
        GameObject loadingSceneView = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_LOADING_SCENE_VIEW);        

        // 오브젝트 생성.
        _loadingObject = Instantiate(loadingSceneView, UIManager.Instance.GetRoot(UIManager.CanvasType.LOADING));

        // 비활성화.
        _loadingObject.SetActive(false);

        // 멤버 변수에 담기.
        _loadingSceneView = _loadingObject.GetComponent<LoadingSceneView>();
        _loadingSceneView.SetProgress(0);

        // 초기화 완료.
        _isInit = true;
    }

    /// <summary>
    /// 씬 이동.
    /// </summary>    
    public async UniTask LoadScene(string sceneName)
    {
        _loadingObject.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // 씬을 즉시 넘기지 않게 false.
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {            
            _loadingSceneView.SetProgress(operation.progress / 0.9f);

            // 씬 준비 완료 됐는지 확인.
            if (operation.progress >= 0.9f)
            {
                // 씬 이동.
                _loadingObject.SetActive(false);
                operation.allowSceneActivation = true;                
            }            

            await UniTask.Yield(PlayerLoopTiming.Update);
        }        
    }
}
