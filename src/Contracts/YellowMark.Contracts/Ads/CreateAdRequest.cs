namespace YellowMark.Contracts.Ads;

/// <summary>
/// Request data model for Ad creation.
/// </summary>
public class CreateAdRequest
{
    /// <summary>
    /// Ad title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Ad decription.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Goods price specified in the ad.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Goods Currency id specified in the ad (<see cref="Guid"/>). 
    /// </summary>
    public Guid? CurrencyId { get; set; }

    /// <summary>
    /// Ad Subcategory id <see cref="Guid"/>.
    /// </summary>
    public Guid CategoryId { get; set; }
}
