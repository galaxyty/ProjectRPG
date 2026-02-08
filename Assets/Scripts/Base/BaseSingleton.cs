using UnityEngine;

// 하이어라키 창에 이미 생성 된 오브젝트에 싱글톤 시킬 스크립트.
public class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {            
            return _instance;
        }
    }

    // 싱글톤 초기화.
    private void Initialzation()
    {
        _instance = this as T;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            Initialzation();
        }

        DontDestroyOnLoad(this);
    }
}
