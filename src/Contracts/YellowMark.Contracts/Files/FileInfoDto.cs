namespace YellowMark.Contracts.Files;

/// <summary>
/// File info data transfer object. 
/// </summary>
public class FileInfoDto
{
    /// <summary>
    /// File identifier. 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// File uploading date.
    /// </summary>
    public DateTime UploadedAt { get; set; }

    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File size.
    /// </summary>
    public int Length { get; set; }
}
