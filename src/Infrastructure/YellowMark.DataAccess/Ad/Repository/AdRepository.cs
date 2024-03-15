using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Categories;
using YellowMark.Contracts.Currnecies;
using YellowMark.Contracts.Subcategories;
using YellowMark.Contracts.Users;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Ad.Repository;

/// <inheritdoc />
public class AdRepository : IAdRepository
{
    /*
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
    */

    /// <inheritdoc />
    public async Task<IEnumerable<AdDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var ads = MockList();
        return await Task.Run(() => ads.Select(ad => new AdDto
        {
            Id = ad.Id,
            CreatedAt = ad.CreatedAt,
            UpdatedAt = ad.UpdatedAt,
            Owner = new UserDto
            {
                Id = Guid.NewGuid(),
                FirstName = "BLob",
                MiddleName = "Jr.",
                LastName = "Awesome",
                FullName = "Awesome Jr. Blob",
                Email = "blob.awesome@email.com",
                Phone = "+71112345678"
            },
            Title = ad.Title,
            Description = ad.Description,
            Category = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Transport",
            },
            Subcategory = new SubcategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Auto",
            },
            Currency = new CurrencyDto
            {
                AlphabeticCode = "RUB",
                NumericCode = 643
            },
            Price = ad.Price
        }), cancellationToken);
        /*
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
        */
    }

    // Mock Ad Data
    private static List<Domain.Ads.Entity.Ad> MockList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                SubcategoryId = Guid.NewGuid(),
                Title = "Awesome iron horse.",
                Description = "Almost new motobike!",
                CurrencyId = Guid.NewGuid(),
                Price = 300_000
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                SubcategoryId = Guid.NewGuid(),
                Title = "Mock Ad",
                Description = "Some mock description...",
                CurrencyId = Guid.NewGuid(),
                Price = 1.36
            },
        ];
    }
}
