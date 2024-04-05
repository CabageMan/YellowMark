namespace YellowMark.Contracts.Pagination;

/// <summary>
/// Request data model for all users request.
/// </summary>
public class GetAllRequestWithPagination
{
    /// <summary>
    /// Current page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of requesting entities. Default value is 10.
    /// </summary>
    public int BatchSize { get; set; } = 10;
}
