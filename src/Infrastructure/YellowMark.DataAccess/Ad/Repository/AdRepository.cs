using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.Contracts.Ads;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Ad.Repository;

/// <inheritdoc />
public class AdRepository : IAdRepository
{
    private readonly IWriteOnlyRepository<Domain.Ads.Entity.Ad> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Ads.Entity.Ad> _readOnlyrepository;

    /// <summary>
    /// Init AdRepository (<see cref="IAdRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public AdRepository(
        IWriteOnlyRepository<Domain.Ads.Entity.Ad> writeOnlyRepository,
        IReadOnlyRepository<Domain.Ads.Entity.Ad> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AdDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var ads = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        // TODO: Find a better way to get add onfo from other databases.
        return ads.Select(ad => new AdDto
        {
            Id = ad.Id,
            CreatedAt = ad.CreatedAt,
            UpdatedAt = ad.UpdatedAt,
            // Owner = ,
            Title = ad.Title,
            Description = ad.Description,
            // Category = ,
            // Subcategory = ,
            // Currency = ,
            Price = ad.Price
        });
    }
}
