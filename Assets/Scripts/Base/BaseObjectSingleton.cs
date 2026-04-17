using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

// 하이어라키 창에 생성하는 싱글톤.
public class BaseObjectSingleton<T> : MonoBehaviour where T : BaseObjectSingleton<T>
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Initialzation();
            }

            return _instance;
        }
    }

    // 싱글톤 초기화.
    private static void Initialzation()
    {
        // 타입.
        Type type = typeof(T);

        // 오브젝트 생성.
        GameObject obj = new GameObject();

        // 오브젝트 이름 재정의.
        obj.name = type.ToString();
        
        // 해당 싱글톤 컴포넌트 생성.
        _instance = obj.AddComponent<T>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 싱글톤 비동기 초기화.
    /// </summary>
    public virtual UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 싱글톤 비동기 해제.
    /// </summary>
    public virtual UniTask DisposeAsync()
    {
        return UniTask.CompletedTask;
    }
}
