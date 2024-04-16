namespace YellowMark.Contracts.Files;


/// <summary>
/// File data transfer object. 
/// </summary>
public class FileDto
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
}
