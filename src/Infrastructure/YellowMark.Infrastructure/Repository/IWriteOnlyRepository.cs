using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Write-only basic repository.
/// </summary>
/// <typeparam name="TEntity">Entity type inherited from <see cref="BaseEntity"/></typeparam>
/// <typeparam name="TContext">DbContext type inherited from <see cref="DbContext"/></typeparam>
public interface IWriteOnlyRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
{
    /// <summary>
    /// Add new instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Update current instance of the entity.
    /// </summary>
    /// <param name="model">Entity model <see cref="TEntity"/></param>
    /// <returns></returns>
    Task UpdateAsync(TEntity model, CancellationToken cancellationToken);

    /// <summary>
    /// Delete an instance of the entity by id.
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
