using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Subcategories.Repositories;
using YellowMark.Contracts.Subcategories;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Subcategory.Repository;

/// <inheritdoc />
public class SubcategoryRepository : ISubcategoryRepository
{
    /*
    private readonly IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory> _readOnlyrepository;

    /// <summary>
    /// Init ISubcategoryRepository (<see cref="ISubcategoryRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public SubcategoryRepository(
        IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory> writeOnlyRepository,
        IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }
    */

    /// <inheritdoc />
    public async Task<IEnumerable<SubcategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subcategories = MockList();
        return await Task.Run(() => subcategories.Select(subcategory => new SubcategoryDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name
        }), cancellationToken);
        /*
        var subcategories = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        return subcategories.Select(subcategory => new SubcategoryDto
        {
           Id = subcategory.Id,
           Name = subcategory.Name 
        });
        */
    }

    // Mock Subcategory Data
    private static List<Domain.Subcategories.Entity.Subcategory> MockList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Auto",
                CategoryId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Moto",
                CategoryId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Gydro",
                CategoryId = Guid.NewGuid()
            }
        ];
    }
}
