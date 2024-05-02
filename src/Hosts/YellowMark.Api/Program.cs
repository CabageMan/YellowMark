using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
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
        builder.Services.AddServices(builder.Configuration);

        builder.Services.AddControllers();

        // Swager
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            {
                SetupSwagerDefault(options);
                SetupSwagerSecurity(options);
            }
        );

        var app = builder.Build();

        app.UseExceptionHandler(); 

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

    // Swager Setup.
    private static void SetupSwagerDefault(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "YellowMark notice board API",
            Version = "v1"
        }
        );
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
    }

    private static void SetupSwagerSecurity(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme.  
                    Enter 'Bearer' [space] and then your token in the text input below.
                    Example: 'Bearer secretKey'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        }
        );
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            }
        );
    }
}
