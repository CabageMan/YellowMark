using Microsoft.OpenApi.Models;
using YellowMark.Api.Controllers;
using YellowMark.ComponentRegistrar;
using YellowMark.Contracts.Users;

namespace YellowMark.Api;

/// <summary>
/// Application entry point.
/// </summary>
public class Program
{
    /// <inheritdoc />
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDependencyGroup();

        builder.Services.AddControllers();

        // Swager
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

        // Add dependencies DB contexts.
        // Research contextConfigurations. It may be userfull for connect several databases.

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
