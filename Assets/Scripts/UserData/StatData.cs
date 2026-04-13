using R3;
using UnityEngine;

public class StatData
{
    /// <summary>
    /// Ã¼·Â.
    /// </summary>
    public ReactiveProperty<int> HP = new();

    /// <summary>
    /// Ã¼·Â È¹µæ ÀÌº¥Æ®.
    /// </summary>
    public Subject<int> OnSetHP = new();

    public StatData()
    {
        OnSetHP
            .Subscribe(hp =>
            {
                Debug.Log($"Ã¼·Â È¹µæ : {hp}");

                HP.Value += hp;
            });
    }
}
