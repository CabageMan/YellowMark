using Microsoft.OpenApi.Models;
using YellowMark.Api.Controllers;
using YellowMark.ComponentRegistrar;
using YellowMark.Contracts.UsersInfos;

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
        builder.Services.AddDependencyGroup(builder.Configuration);

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
                    AppContext.BaseDirectory, $"{typeof(UserInfoController).Assembly.GetName().Name}.xml"
                ))
            );
            options.IncludeXmlComments(
                Path.Combine(Path.Combine(
                    AppContext.BaseDirectory, $"{typeof(UserInfoDto).Assembly.GetName().Name}.xml"
                ))
            );
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
