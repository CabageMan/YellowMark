using YellowMark.AppServices.Categories.Repositories;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Categories;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.ApiTests.Categories;

/// <summary>
/// Stub for <see cref="ICategoryRepository"/>
/// </summary>
public class CategoryRepositoryStub : ICategoryRepository
{
    /// <summary>
    /// Mock all categories.
    /// </summary>
    public static CategoryDto[] AllCategories { get; } = new[] {
        new CategoryDto { Id = Guid.NewGuid(), Name = "TestCategory1"},
        new CategoryDto { Id = Guid.NewGuid(), Name = "TestCategory2"},
        new CategoryDto { Id = Guid.NewGuid(), Name = "TestCategory3"}
    };

    /// <inheritdoc/>
    public Task AddAsync(Category entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> ExistsWithId(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return AllCategories;
    }

    /// <inheritdoc/>
    public Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<CategoryDto>> GetFiltered(Specification<Category> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task UpdateAsync(Category entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
