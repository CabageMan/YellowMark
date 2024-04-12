﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Services;
using YellowMark.AppServices.Validators;
using YellowMark.DataAccess.User.Repository;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;
using YellowMark.AppServices.Subcategories.Repositories;
using YellowMark.DataAccess.Subcategory.Repository;
using YellowMark.AppServices.Subcategories.Services;
using YellowMark.AppServices.Categories.Services;
using YellowMark.DataAccess.Category.Repository;
using YellowMark.AppServices.Categories.Repositories;
using YellowMark.AppServices.Currencies.Repositories;
using YellowMark.DataAccess.Currency.Repository;
using YellowMark.AppServices.Ads.Repositories;
using YellowMark.DataAccess.Ad.Repository;
using YellowMark.AppServices.Currencies.Services;
using YellowMark.AppServices.Ads.Services;
using YellowMark.ComponentRegistrar.MapperProfiles;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.DataAccess.Comment.Repository;
using YellowMark.AppServices.Comments.Services;

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
        services.AddTransient<ISubcategoryRepository, SubcategoryRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IAdRepository, AdRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
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
        services.AddTransient<ISubcategoryService, SubcategoryService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICurrencyService, CurrencyService>();
        services.AddTransient<IAdService, AdService>();
        services.AddTransient<ICommentService, CommentService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }

    // Helpers
    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<UserProfile>();
            config.AddProfile<SubcategoryProfile>();
            config.AddProfile<CategoryProfile>();
            config.AddProfile<CurrencyProfile>();
            config.AddProfile<AdProfile>();
            config.AddProfile<CommentProfile>();
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
