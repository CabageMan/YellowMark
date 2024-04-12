namespace YellowMark.Contracts.Currnecies;

/// <summary>
/// Request data model for currency creation.
/// </summary>
public class CreateCurrencyRequest
{
    /// <summary>
    /// International alphabetic currency code. 
    /// </summary>
    public string AlphabeticCode { get; set; }

    /// <summary>
    /// International numeric currency code. 
    /// </summary>
    public int NumericCode { get; set; }
}
