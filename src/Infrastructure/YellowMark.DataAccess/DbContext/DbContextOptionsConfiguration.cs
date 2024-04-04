using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Database context configuration.
/// </summary>
public class DbContextOptionsConfiguration<TContext> : IDbContextOptionsConfigurator<TContext> where TContext : DbContext
{
    private const string WriteConnectionStringName = "WriteDB";
    private const string ReadConnectionStringName = "ReadDB";

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Creates instance of <see cref="YellowMarkDbContextConfiguration"/> 
    /// </summary>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public DbContextOptionsConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<TContext> optionsBuilder)
    {
        var connectionStringName = typeof(TContext) == typeof(WriteDbContext)
            ? WriteConnectionStringName
            : typeof(TContext) == typeof(ReadDbContext)
            ? ReadConnectionStringName : "";

        var connectionString = _configuration.GetConnectionString(connectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{connectionStringName}' not found."
            );
        }
        
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
        {
            Username = _configuration.GetSection("DbConnection")["Username"],
            Password = _configuration.GetSection("DbConnection")["Password"]
        };

        //  TODO: Handle Exceptions in controllers when DB string is empty.
        //  TODO: Handle Exceptions when database does not exist.

        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
    }
}
