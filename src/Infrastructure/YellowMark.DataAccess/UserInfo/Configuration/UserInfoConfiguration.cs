using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YellowMark.Domain.UsersInfos.Entity;

namespace YellowMark.DataAccess.User.Configuration;

/// <summary>
/// Users table configuration.
/// </summary>
public class UserInfoConfiguration : IEntityTypeConfiguration<Domain.UsersInfos.Entity.UserInfo>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.UsersInfos.Entity.UserInfo> builder)
    {
        builder
            .ToTable("UsersInfos")
            .HasKey(user => user.Id);

        builder
            .Property(user => user.CreatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(user => user.UpdatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(user => user.MiddleName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(user => user.BirthDate)
            .IsRequired();

        builder
            .Property(user => user.Phone)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(user => user.ShowPhone)
            .IsRequired();
        
        builder
            .HasMany(user => user.Ads)
            .WithOne(ad => ad.UserInfo)
            .HasForeignKey(ad => ad.UserInfoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(user => user.Comments)
            .WithOne(comment => comment.UserInfo)
            .HasForeignKey(comment => comment.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
