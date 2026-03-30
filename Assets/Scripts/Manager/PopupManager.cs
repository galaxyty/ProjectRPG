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

    // 로컬 초기화 여부.
    private bool _isLocalInit = false;

    /// <summary>
    /// 어드레서블 로컬 팝업 초기화.
    /// </summary>
    public async UniTask InitializationLocal()
    {
        if (_isLocalInit == true) return;

        // 팝업 부모 트랜스폼.
        _parent = UIManager.Instance.GetRoot(UIManager.CanvasType.POPUP);

        // 리소스 로딩 팝업.
        var popup = await ResourceManager.Instance.LoadAsync<GameObject>(Consts.kPATH_RESOURCE_DOWNLOAD_POPUP);
        var component = popup.GetComponent<ResourceDownloadPopup>();

        // 캐싱.
        _cachingPopup.Add(component.GetType(), component);

        // 초기화 완료.
        _isLocalInit = true;
    }

    /// <summary>
    /// T 타입 팝업 오픈.
    /// </summary>
    public void Show<T>() where T : BasePopup
    {
        if (_cachingPopup.TryGetValue(typeof(T), out var basePoup))
        {
        }

        var findTransform = _parent.Find(typeof(T).ToString());

        // 생성되지 않았다면 생성.
        if (findTransform == null)
        {
            if (basePoup != null)
            {
                Instantiate(basePoup, _parent);
            }
        }

        basePoup.gameObject.SetActive(true);
    }
}
