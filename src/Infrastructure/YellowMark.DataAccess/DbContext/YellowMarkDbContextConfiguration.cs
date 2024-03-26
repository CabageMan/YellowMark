using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YellowMark.DataAccess.YellowMarkDbContext;

/// <summary>
/// Data Base context configuration.
/// </summary>
public class YellowMarkDbContextConfiguration : IDbContextOptionsConfigurator<YellowMarkDbContext>
{
    private const string PostgressWriteConnectionStringName = "WriteDB";
    private const string PostgressReadConnectionStringName = "ReadDB";

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Creates instance of <see cref="YellowMarkDbContextConfiguration"/> 
    /// </summary>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public YellowMarkDbContextConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(DbContextOptionsBuilder<YellowMarkDbContext> optionsBuilder)
    {
        var writeConnectionString = _configuration
            .GetConnectionString(PostgressWriteConnectionStringName);
        var readConnectionString = _configuration
            .GetConnectionString(PostgressReadConnectionStringName);

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

        optionsBuilder.UseNpgsql(writeConnectionString);
        optionsBuilder.UseNpgsql(readConnectionString);
    }
}
