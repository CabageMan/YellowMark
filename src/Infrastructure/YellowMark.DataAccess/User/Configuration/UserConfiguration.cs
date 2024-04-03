using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.User.Configuration;

/// <summary>
/// Users table configuration.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Domain.Users.Entity.User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Domain.Users.Entity.User> builder)
    {
        builder
            .ToTable("Users")
            .HasKey(user => user.Id);

        builder
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(user => user.MiddleName)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(user => user.Phone)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(user => user.BirthDate)
            .IsRequired();
    }
}
