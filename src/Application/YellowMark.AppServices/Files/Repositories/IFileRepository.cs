using YellowMark.Contracts.Files;

namespace YellowMark.AppServices.Files.Repositories;

/// <summary>
/// Files repository.
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Upload file.
    /// </summary>
    /// <param name="model">File model <see cref="Domain.Files.Entity.File"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    Task UploadAsync(Domain.Files.Entity.File file, CancellationToken cancellationToken);

    /// <summary>
    /// Download file by id.
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>FileDto</returns>
    Task<FileDto> DownloadByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Get file info by id. 
    /// </summary>
    /// <param name="id">File id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    /// <returns>File info.</returns>
    Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Delete file by id.
    /// </summary>
    /// <param name="id">File to delete id <see cref="Guid"/></param>
    /// <param name="cancellationToken">Operation cancelation token.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}
