using YellowMark.Domain.Base;

namespace YellowMark.Domain.Files.Entity;

/// <summary>
/// Class for File Entity.
/// </summary>
public class File : BaseEntity
{
    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File content.
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    /// File content type.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// File size.
    /// </summary>
    public int Length { get; set; }
}
