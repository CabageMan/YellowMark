
using Microsoft.OpenApi.Models;
using YellowMark.Api.Controllers;
using YellowMark.AppServices.Users.Repositories;
using YellowMark.AppServices.Users.Services;
using YellowMark.Contracts.Users;
using YellowMark.DataAccess.User.Repository;

namespace YellowMark.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "YellowMark notice board API",
                Version = "v1"
            });
            options.IncludeXmlComments(
                Path.Combine(Path.Combine(
                    AppContext.BaseDirectory, $"{typeof(UserController).Assembly.GetName().Name}.xml"
                ))
            );
            options.IncludeXmlComments(
                Path.Combine(Path.Combine(
                    AppContext.BaseDirectory, $"{typeof(UserDto).Assembly.GetName().Name}.xml"
                ))
            );
        });

        // Check if we can add a dependency for DataAccess to inject UserRepository.
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IUserService, UserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
