namespace YellowMark.Contracts.Ads;

/// <summary>
/// Request data model for getting ads by title.
/// </summary>
public class AdByTitleRequest
{
    /// <summary>
    /// Ad title. 
    /// </summary>
    public string Title { get; set; }
}
