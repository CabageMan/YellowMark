using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Services;
using YellowMark.AppServices.Validators;
using YellowMark.DataAccess.User.Repository;
using YellowMark.DataAccess.YellowMarkDbContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.ComponentRegistar;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/> to add dependecies.
/// </summary>
public static class YellowMarkRegistar
{
    /// <summary>
    /// Add dependecies to the project. 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
    {
        // Db Context
        services.AddSingleton<IDbContextOptionsConfigurator<YellowMarkDbContext>, YellowMarkDbContextConfiguration>();

        services.AddDbContext<YellowMarkDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<YellowMarkDbContext>>()
                .Configure((DbContextOptionsBuilder<YellowMarkDbContext>)dbOptions)));

        services.AddScoped((Func<IServiceProvider, DbContext>) (sp => sp.GetRequiredService<YellowMarkDbContext>()));

        // Repositories
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
        services.AddScoped(typeof(IWriteOnlyRepository<>), typeof(WriteOnlyRepository<>));

        services.AddTransient<IUserRepository, UserRepository>();
        // services.AddScoped<IUserRepository, UserRepository>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            
        // Services 
        services.AddTransient<IUserService, UserService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }
}
