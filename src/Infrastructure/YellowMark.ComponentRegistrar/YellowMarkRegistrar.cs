using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.AppServices.UsersInfos.Repositories;
using YellowMark.AppServices.UsersInfos.Services;
using YellowMark.AppServices.Validators;
using YellowMark.DataAccess.UserInfo.Repository;
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
using YellowMark.AppServices.Files.Repositories;
using YellowMark.DataAccess.File.Repository;
using YellowMark.AppServices.Files.Services;
using YellowMark.Domain.Accounts.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using YellowMark.ComponentRegistrar.JwtConfigurator;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YellowMark.AppServices.Accounts.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using YellowMark.ComponentRegistrar.ExceptionHandlers;

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
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.ConfigureExceptionHandler();
        services.ConfigureAutomapper();
        services.ConfigureDbContext();
        services.ConfigureAuthServices(configuration);
        services.ConfigureRepositories();
        services.ConfigureValidators();
        services.ConfigureSrvices();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.ConfigureSerilog(configuration);
        services.ConfigureCaching(configuration);

        return services;
    }

    // Helpers.
    private static IServiceCollection ConfigureExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<AccountExceptionHandler>();
        services.AddExceptionHandler<UserInfoExceptionHandler>();
        // services.AddExceptionHandler<GlobalExceptionHandler>();

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
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IAdRepository, AdRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        return services;
    }

    private static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();

        return services;
    }

    private static IServiceCollection ConfigureSrvices(this IServiceCollection services)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IUserInfoService, UserInfoService>();
        services.AddTransient<ISubcategoryService, SubcategoryService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<ICurrencyService, CurrencyService>();
        services.AddTransient<IAdService, AdService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<IFileService, FileService>();

        return services;
    }

    // Helpers
    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<AccountProfile>();
            config.AddProfile<UserProfile>();
            config.AddProfile<SubcategoryProfile>();
            config.AddProfile<CategoryProfile>();
            config.AddProfile<CurrencyProfile>();
            config.AddProfile<AdProfile>();
            config.AddProfile<CommentProfile>();
            config.AddProfile<FileProfile>();
        });

        configuration.AssertConfigurationIsValid(); // Important to check automappers on App start.

        return configuration;
    }

    private static IServiceCollection AddDbContext<TContext>(this IServiceCollection services) where TContext : IdentityDbContext<YellowMark.Domain.Accounts.Entity.Account, IdentityRole<Guid>, Guid>
    {
        services.AddSingleton<
            IDbContextOptionsConfigurator<TContext>,
            DbContextOptionsConfiguration<TContext>
        >();

        services.AddDbContext<TContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
            ((sp, dbOptions) => sp
                .GetRequiredService<IDbContextOptionsConfigurator<TContext>>()
                .Configure((DbContextOptionsBuilder<TContext>)dbOptions)
            )
        );

        services
            .AddScoped((Func<IServiceProvider, DbContext>)(sp =>
                    sp.GetRequiredService<TContext>()
                )
            );

        return services;
    }

    private static IServiceCollection ConfigureAuthServices(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Identity
        services
            .AddIdentity<Account, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<WriteDbContext>()
            .AddDefaultTokenProviders();

        // Authentication
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((Action<JwtBearerOptions>)
                (options => JwtBearerOptionsConfigurator.Configure(options, configuration))
            );

        // Authorization.
        services.AddAuthorization();

        return services;
    }

    private static IServiceCollection ConfigureSerilog(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSerilog((services, config) =>
        {
            config
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.Debug();
        });

        return services;
    }

    private static IServiceCollection ConfigureCaching(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMemoryCache();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "local_";
        });

        return services;
    }
}
