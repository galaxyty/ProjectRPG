using System;
using UnityEngine;

public class UIManager : BaseSingleton<UIManager>
{
    // Äµ¹ö½º Å¸ÀÔ.
    public enum CanvasType
    {
        UI,             // UI Äµ¹ö½º.
        POPUP,          // ÆË¾÷ Äµ¹ö½º.
        LOADING,        // ·Îµù Äµ¹ö½º. 
    }

    // UI Äµ¹ö½º.
    [SerializeField]
    private Canvas _uiCanvas;

    // ÆË¾÷ Äµ¹ö½º.
    [SerializeField]
    private Canvas _popupCanvas;

    // ·Îµù Äµ¹ö½º.
    [SerializeField]
    private Canvas _loadingCanvas;

    /// <summary>
    /// Äµ¹ö½º ºÎ¸ð Æ®·£½ºÆû ¹ÝÈ¯.
    /// </summary>
    public Transform GetRoot(CanvasType type)
    {
        return type switch
        {
            CanvasType.UI => _uiCanvas.transform,
            CanvasType.POPUP => _popupCanvas.transform,
            CanvasType.LOADING => _loadingCanvas.transform,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
