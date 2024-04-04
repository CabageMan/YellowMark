using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Services;
using YellowMark.AppServices.Validators;
using YellowMark.DataAccess.User.Repository;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.ComponentRegistrar;

/// <summary>
/// Extension class for <see cref="IServiceCollection"/> to add dependecies.
/// </summary>
public static class YellowMarkRegistrar
{
    /// <summary>
    /// Add dependecies to the project. 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
    {
        services.ConfigureAutomapper();
        services.ConfigureDbContext();
        services.ConfigureRepositories();
        services.ConfigureValidators();
        services.ConfigureSrvices();

        return services;
    }

    private static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
    {
        return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
    }

    private static IServiceCollection ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<WriteDbContext>();
        services.AddDbContext<ReadDbContext>();

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
        services.AddScoped(typeof(IWriteOnlyRepository<,>), typeof(WriteOnlyRepository<,>));
        services.AddTransient<IUserRepository, UserRepository>();
        // services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

        return services;
    }

    private static IServiceCollection ConfigureSrvices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }

    // Helpers
    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<UserProfile>();
        });

        configuration.AssertConfigurationIsValid(); // Important to check automappers on App start.

        return configuration;
    }

    private static IServiceCollection AddDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddSingleton<
            IDbContextOptionsConfigurator<TContext>,
            DbContextOptionsConfiguration<TContext>
        >();

        services.AddDbContext<TContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<TContext>>()
                .Configure((DbContextOptionsBuilder<TContext>)dbOptions)));

        services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<TContext>()));

        return services;
    }
}
