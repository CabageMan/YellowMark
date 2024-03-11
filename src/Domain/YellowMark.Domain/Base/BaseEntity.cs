namespace YellowMark.Domain.Base;

/// <summary>
/// Base class for all entities.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// User record identifier. 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User record creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}