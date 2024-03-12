namespace YellowMark.Domain.Base;

/// <summary>
/// Base class for all entities.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Record identifier. 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Record creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Record update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}