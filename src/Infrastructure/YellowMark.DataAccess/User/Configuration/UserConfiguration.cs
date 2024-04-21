﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YellowMark.Domain.Users.Entity;

namespace YellowMark.DataAccess.User.Configuration;

/// <summary>
/// Users table configuration.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Domain.Users.Entity.User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Users.Entity.User> builder)
    {
        builder
            .ToTable("Users")
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
        //     .HasForeignKey<Domain.Users.Entity.User>(user => user.AccountId)
        //     .IsRequired();
        
        builder
            .HasMany(user => user.Ads)
            .WithOne(ad => ad.User)
            .HasForeignKey(ad => ad.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(user => user.Comments)
            .WithOne(comment => comment.User)
            .HasForeignKey(comment => comment.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
