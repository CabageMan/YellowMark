using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Categories.Repositories;
using YellowMark.Contracts.Categories;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Category.Repository;

/// <inheritdoc />
public class CategoryRepository : ICategoryRepository
{
    private readonly IWriteOnlyRepository<Domain.Categories.Entity.Category> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Categories.Entity.Category> _readOnlyrepository;

    /// <summary>
    /// Init CategoryRepository (<see cref="ICategoryRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public CategoryRepository(
        IWriteOnlyRepository<Domain.Categories.Entity.Category> writeOnlyRepository,
        IReadOnlyRepository<Domain.Categories.Entity.Category> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        });
    }
}
