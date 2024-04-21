using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using YellowMark.DataAccess.DatabaseContext.AuthContext;

namespace YellowMark.DataAccess;

/// <summary>
/// Database context configuration for Identity.
/// </summary>
public class AuthDbContextOptionsConfigurator : IAuthDbContextOptionsConfigurator
{
    private const string ConnectionStringName = "WriteDB";
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Creates instance of <see cref="AuthDbContextOptionsConfigurator"/> 
    /// </summary>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public AuthDbContextOptionsConfigurator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public void Configure(DbContextOptionsBuilder<AuthDbContext> optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' not found."
            );
        }

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Username = _configuration.GetSection("DbConnection")["Username"],
            Password = _configuration.GetSection("DbConnection")["Password"]
        };

        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
    }
}
