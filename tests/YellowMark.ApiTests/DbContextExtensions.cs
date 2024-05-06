using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YellowMark.DataAccess.DatabaseContext;

namespace YellowMark.ApiTests;

/// <summary>
/// Extensions for DBContexts.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Initialize <see cref="IdentityDbContext"/> with entities.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="identityDbContext"><see cref="IdentityDbContext"/></param>
    /// <param name="entities">Collection on entities.</param>
    /// <returns></returns>
    public static async Task InitializeWithAsync<T>(this IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid> identityDbContext, IEnumerable<T> entities) where T : class
    {
        await identityDbContext.Database.EnsureDeletedAsync();
        await identityDbContext.Database.EnsureCreatedAsync();

        var entitySet = identityDbContext.Set<T>();
        entitySet.RemoveRange(entitySet.AsNoTracking().ToList());
        entitySet.AddRange(entities);

        await identityDbContext.SaveChangesAsync();
    }
}
