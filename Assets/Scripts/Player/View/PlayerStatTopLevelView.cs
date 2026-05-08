using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatTopLevelView : BaseView<PlayerStatTopLevelPresenter>
{
    [SerializeField]
    private Text _txtLevel;

    public override UniTask InitializationAsync()
    {
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 레벨 텍스트 갱신.
    /// </summary>
    public void SetLevel(string level)
    {
        _txtLevel.text = level;
    }
}
