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
        if (_isBackButton == true)
        {
            // 백버튼 적용시에는 팝업매니저에서 닫기.
            PopupManager.Instance.Close();
        }
        else
        {
            // 백버튼 미 적용시에는 그냥 비활성화.
            gameObject.SetActive(false);
        }
    }
}
