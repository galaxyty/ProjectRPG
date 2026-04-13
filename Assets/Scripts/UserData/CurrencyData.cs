using R3;
using UnityEngine;

public class CurrencyData
{
    /// <summary>
    /// °ñµå.
    /// </summary>
    public ReactiveProperty<double> Gold = new();

    /// <summary>
    /// °ñµå È¹µæ ÀÌº¥Æ®.
    /// </summary>
    public Subject<double> OnGetGold = new();

    public CurrencyData()
    {
        OnGetGold
            .Subscribe(gold =>
            {
                Debug.Log($"°ñµå È¹µæ : {gold}¿ø");

                Gold.Value += gold;
            });
    }
}
