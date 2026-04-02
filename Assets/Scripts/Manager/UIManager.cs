using System;
using UnityEngine;

// 씬 공용 관리되는 UI 매니저.
public class UIManager : BaseSingleton<UIManager>
{
    // 캔버스 타입.
    public enum CanvasType
    {
        POPUP,          // 팝업 캔버스.
        LOADING,        // 로딩 캔버스. 
    }

    // 팝업 캔버스.
    [SerializeField]
    private Canvas _popupCanvas;

    // 로딩 캔버스.
    [SerializeField]
    private Canvas _loadingCanvas;

    void Start()
    {
        // 공용 캔버스들을 DontDestroyOnLoad 해줄 것.
        DontDestroyOnLoad(_popupCanvas);
        DontDestroyOnLoad(_loadingCanvas);
    }

    /// <summary>
    /// 캔버스 부모 트랜스폼 반환.
    /// </summary>
    public Transform GetRoot(CanvasType type)
    {
        return type switch
        {
            CanvasType.POPUP => _popupCanvas.transform,
            CanvasType.LOADING => _loadingCanvas.transform,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
