using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YellowMark.Domain.Base;

namespace YellowMark.Infrastructure.Repository;

/// <summary>
/// Read-only repository.
/// </summary>
/// <typeparam name="TEntity">Entity type inherited from <see cref="BaseEntity"/></typeparam>
/// <typeparam name="TContext">DbContext type inherited from <see cref="IdentityDbContext"/></typeparam>
public interface IReadOnlyRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
{

    /// <summary>
    /// Returns all instances of the entity <see cref="TEntity"/>.
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Returns an instance of the entity by id.
    /// </summary>
    /// <param name="id">Entity id <see cref="Guid"/></param>
    /// <returns><see cref="TEntity"/></returns>
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
