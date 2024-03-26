using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Services;
using YellowMark.AppServices.Validators;
using YellowMark.DataAccess.User.Repository;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.ComponentRegistar;

public static class YellowMarkRegistar
{
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
    {
        // Db Context

        // Repositories
        services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
        services.AddScoped(typeof(IWriteOnlyRepository<>), typeof(WriteOnlyRepository<>));

        services.AddTransient<IUserRepository, UserRepository>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            
        // Services 
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
