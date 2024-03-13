namespace YellowMark.Contracts.Currnecies;

/// <summary>
/// Currency data trancser oject.
/// </summary>
public class CurrencyDto
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