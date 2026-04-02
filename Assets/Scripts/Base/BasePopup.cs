using UnityEngine;

public abstract class BasePopup : MonoBehaviour
{
    // 백버튼 적용 여부.
    protected bool _isBackButton = true;

    /// <summary>
    /// 백 버튼 적용 여부.
    /// </summary>
    public bool isBackButton
    {
        get { return _isBackButton; }
        private set { }
    }

    /// <summary>
    /// 팝업 닫기.
    /// </summary>
    protected void Close()
    {
        PopupManager.Instance.Close();
    }
}
