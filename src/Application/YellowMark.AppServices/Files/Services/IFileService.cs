using YellowMark.Contracts.Files;

namespace YellowMark.AppServices.Files.Services;

/// <summary>
/// File service.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Upload file.
    /// </summary>
    /// <param name="model">File model <see cref="FileDto"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>Uploaded file id.</returns>
    Task<Guid> UploadFileAsync(FileDto model, CancellationToken cancellationToken);

    /// <summary>
    /// Download file by id.
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>FileDto</returns>
    Task<FileDto> DownloadFileByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Get file info by id. 
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>File info.</returns>
    Task<FileInfoDto> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Delete file by id.
    /// </summary>
    /// <param name="id">File to delete id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    Task DeleteFileByIdAsync(Guid id, CancellationToken cancellationToken);
}
