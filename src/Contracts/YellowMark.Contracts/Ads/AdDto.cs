using YellowMark.Contracts.Users;

namespace YellowMark.Contracts.Ads;

public class AdDto
{
    /// <summary>
    /// Ad record identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Ad creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Ad update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Ad owner <see cref="UserDto"/>
    /// </summary>
    public UserDto Owner { get; set; }

    /// <summary>
    /// Ad description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Goods currency specified in the ad. 
    /// </summary>
    public CurrencyDto Currency { get; set; }
}