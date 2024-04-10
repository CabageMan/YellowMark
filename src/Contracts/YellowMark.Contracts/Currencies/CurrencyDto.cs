namespace YellowMark.Contracts.Currnecies;

/// <summary>
/// Currency data trancser oject.
/// </summary>
public class CurrencyDto
{
    /// <summary>
    /// Currency record identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Currency creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Currency update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// International alphabetic currency code. 
    /// </summary>
    public string AlphabeticCode { get; set; }

    /// <summary>
    /// International numeric currency code. 
    /// </summary>
    public int NumericCode { get; set; }
}