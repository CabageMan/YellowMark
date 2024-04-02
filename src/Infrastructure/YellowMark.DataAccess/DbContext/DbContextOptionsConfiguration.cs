using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace YellowMark.DataAccess.DatabaseContext;

/// <summary>
/// Database context configuration.
/// </summary>
public class DbContextOptionsConfiguration : IDbContextOptionsConfigurator<WriteDbContext>
{
    private const string PostgressWriteConnectionStringName = "WriteDB";
    private const string PostgressReadConnectionStringName = "ReadDB";

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
    public void Configure(DbContextOptionsBuilder<WriteDbContext> optionsBuilder)
    {
        var writeConnectionString = _configuration
            .GetConnectionString(PostgressWriteConnectionStringName);
        var readConnectionString = _configuration
            .GetConnectionString(PostgressReadConnectionStringName);
        
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder(writeConnectionString)
        {
            Username = _configuration.GetSection("DbConnection")["Username"],
            Password = _configuration.GetSection("DbConnection")["Password"]
        };

        //  TODO: Handle Exceptions in controllers when DB string is empty.
        //  TODO: Handle Exceptions when database does not exist.

        if (string.IsNullOrEmpty(writeConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressWriteConnectionStringName}' not found."
            );
        }

        if (string.IsNullOrEmpty(readConnectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{PostgressReadConnectionStringName}' not found."
            );
        }

        optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);
        // TODO: Set write and read contexts.
    }
}
