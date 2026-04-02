using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        // 이벤트 구독.
        Observable.EveryUpdate()
        .Where(_ => Input.GetKeyDown(KeyCode.Escape))
        .Subscribe(_ => Close())
        .AddTo(this);

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

        // null 체크.
        if (basePopup == null)
        {
            return null;
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

                // 백버튼 적용된거에만 푸시.
                if (component.isBackButton == true)
                {
                    _popupStack.Push(component);
                }                

                return component;
            }
        }

        basePopup.gameObject.SetActive(true);

        // 백버튼 적용된거에만 푸시.
        if (basePopup.isBackButton == true)
        {
            _popupStack.Push(basePopup);
        }        

        return basePopup as T;
    }

    /// <summary>
    /// 팝업 닫기.
    /// </summary>
    public void Close()
    {        
        if (_popupStack.Count == 0)
        {
            return;
        }

        var popup = _popupStack.Pop();
        popup.gameObject.SetActive(false);
    }
}
