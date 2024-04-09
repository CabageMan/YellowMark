using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.Comment.Configuration;

/// <summary>
/// Comments table configuration.
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Domain.Comments.Entity.Comment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Comments.Entity.Comment> builder)
    {
        builder
            .ToTable("Comments")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.CreatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(c => c.UpdatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(1024);
    }
}
