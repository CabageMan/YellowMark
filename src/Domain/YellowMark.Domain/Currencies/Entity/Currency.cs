using YellowMark.Domain.Ads.Entity;
using YellowMark.Domain.Base;

namespace YellowMark.Domain.Currencies.Entity;

/// <summary>
/// Class for Currency 
/// </summary>
public class Currency : BaseEntity
{
    /// <summary>
    /// International alphabetic currency code. 
    /// </summary>
    public string AlphabeticCode { get; set; }

    /// <summary>
    /// International numeric currency code. 
    /// </summary>
    public int NumericCode { get; set; }

    public virtual List<Ad> Ads { get; set; }
}