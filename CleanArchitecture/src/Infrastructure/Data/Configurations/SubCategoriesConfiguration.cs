using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class SubCategoriesConfiguration : IEntityTypeConfiguration<SubCategories>
{
    public void Configure(EntityTypeBuilder<SubCategories> builder)
    {
        builder.HasIndex(x => new { x.Name });
        builder.HasOne(x => x.Categories).WithMany(x => x.SubCategories).HasForeignKey(x => x.CategoryId);
        builder.Property(x => x.Name).HasMaxLength(50);
    }
}
