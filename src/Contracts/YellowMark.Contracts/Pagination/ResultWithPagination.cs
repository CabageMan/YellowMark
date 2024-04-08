namespace YellowMark.Contracts.Pagination;

/// <summary>
/// Generic pagination result model.
/// </summary>
/// <typeparam name="T">Result type given to client.</typeparam>
public class ResultWithPagination<T> where T : class
{
    /// <summary>
    /// Collection of result entities.
    /// </summary>
    public IEnumerable<T> Result { get; set; }

    /// <summary>
    /// The number of available pages.
    /// </summary>
    public int AvailablePages { get; set; }
}
