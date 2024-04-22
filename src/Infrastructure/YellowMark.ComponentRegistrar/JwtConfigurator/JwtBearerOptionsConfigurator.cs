using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace YellowMark.ComponentRegistrar.JwtConfigurator;

/// <summary>
/// JWT bearer options configurator.
/// </summary>
public static class JwtBearerOptionsConfigurator
{
    /// <summary>
    /// Configure JWT bearer options.
    /// </summary>
    /// <param name="options">JWT bearer options <see cref="JwtBearerOptions"/></param>
    public static void Configure(JwtBearerOptions options, ConfigurationManager configuration)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        // TODO: Check for null value.
        var secretKey = configuration.GetSection("Jwt")["SecretKey"] ?? "";

        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            )
        };
    }
}
