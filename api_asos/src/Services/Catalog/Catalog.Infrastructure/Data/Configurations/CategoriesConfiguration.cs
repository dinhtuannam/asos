using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Data.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
			builder.HasQueryFilter(p => p.DeleteFlag != true);
			builder.HasOne(x => x.Gender).WithMany(x => x.Categories).HasForeignKey(x => x.GenderId);
        }
    }
}
