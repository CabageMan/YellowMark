using AutoMapper;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.AppServices.Ads.Services;
using YellowMark.AppServices.Files.Exceptions;
using YellowMark.AppServices.Files.Repositories;
using YellowMark.Contracts.Files;

namespace YellowMark.AppServices.Files.Services;

/// <inheritdoc cref="IFileService"/>
public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IAdService _adService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="FileService"/> instance.
    /// </summary>
    /// <param name="fileRepository">File repository <see cref="IFileRepository"/></param>
    /// <param name="adService">Account service <see cref="IAdService"/></param>
    /// <param name="mapper">Ads mapper.</param>
    public FileService(
        IFileRepository fileRepository, 
        IAdService adService,
        IMapper mapper)
    {
        _fileRepository = fileRepository;
        _adService = adService;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> UploadFileAsync(FileDto model, CancellationToken cancellationToken)
    {
        var targetAdExists = await _adService.AdExistsWithId(model.AdId, cancellationToken);
        FileOperationException.ThrowIfFalse(targetAdExists, $"There is no ad with id: {model.AdId}");

        var file = _mapper.Map<FileDto, Domain.Files.Entity.File>(model);
        await _fileRepository.UploadAsync(file, cancellationToken);
        return file.Id;
    }

    /// <inheritdoc/>
    public Task<FileDto> DownloadFileByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _fileRepository.DownloadByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<FileInfoDto> GetFileInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _fileRepository.GetInfoByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DeleteFileByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _fileRepository.DeleteByIdAsync(id, cancellationToken);
    }
}
