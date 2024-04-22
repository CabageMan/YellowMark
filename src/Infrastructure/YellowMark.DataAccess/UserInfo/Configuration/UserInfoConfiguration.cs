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
            .Property(user => user.BirthDate)
            .IsRequired();

        // builder
        //     .HasOne(user => user.Account)
        //     .WithOne(acc => acc.User)
        //     .HasForeignKey<Domain.UsersInfos.Entity.UserInfo>(user => user.AccountId)
        //     .IsRequired();
        
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
