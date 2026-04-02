using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PopupManager : BaseObjectSingleton<PopupManager>
{
    // 현재 팝업 스택.
    private Stack<BasePopup> _popupStack = new();

    // 팝업 캐싱.
    private Dictionary<Type, BasePopup> _cachingPopup = new();

    // 팝업 캔버스.
    private Transform _parent;

    /// <summary>
    /// 어드레서블 로컬 팝업 초기화.
    /// </summary>
    public async UniTask InitializationLocal()
    {
        // 팝업 부모 트랜스폼.
        _parent = UIManager.Instance.GetRoot(UIManager.CanvasType.POPUP);

        // 리소스 로딩 팝업.
        var popup = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_RESOURCE_DOWNLOAD_POPUP);
        var component = popup.GetComponent<ResourceDownloadPopup>();

        // 캐싱.
        _cachingPopup.Add(component.GetType(), component);
    }

    /// <summary>
    /// T 타입 팝업 오픈.
    /// </summary>
    public T Show<T>() where T : BasePopup
    {
        if (_cachingPopup.TryGetValue(typeof(T), out var basePopup))
        {
        }

        var findTransform = _parent.Find(typeof(T).ToString());

        // 생성되지 않았다면 생성.
        if (findTransform == null)
        {
            if (basePopup != null)
            {
                var obj = Instantiate(basePopup, _parent);
                var component = obj.GetComponent<T>();
                component.gameObject.SetActive(true);

                return component;
            }
        }

        basePopup.gameObject.SetActive(true);

        return basePopup as T;
    }
}
