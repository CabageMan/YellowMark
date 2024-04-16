using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Files.Repositories;
using YellowMark.Contracts.Files;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.File.Repository;

/// <inheritdoc cref="IFileRepository"/>
public class FileRepository : IFileRepository
{
    private readonly IWriteOnlyRepository<Domain.Files.Entity.File, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Files.Entity.File, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init FileRepository (<see cref="IFileRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public FileRepository(
        IWriteOnlyRepository<Domain.Files.Entity.File, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Files.Entity.File, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task UploadAsync(Domain.Files.Entity.File file, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(file, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<FileDto> DownloadByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _readOnlyRepository
            .GetAll()
            .Where(f => f.Id == id)
            .ProjectTo<FileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _readOnlyRepository
            .GetAll()
            .Where(f => f.Id == id)
            .ProjectTo<FileInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }

}
