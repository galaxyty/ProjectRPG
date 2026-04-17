using R3;

public class CurrencyUserData : BaseUserData
{
    /// <summary>
    /// °ñµå.
    /// </summary>
    public ReactiveProperty<double> Gold { get; private set; } = new();

    public override void Initialization()
    {        
    }
}
