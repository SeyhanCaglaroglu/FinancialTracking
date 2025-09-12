using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTracking.Persistence.Features.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Created)
                   .IsRequired();

            builder.Property(c => c.Updated)
                   .IsRequired(false);

            builder.HasMany(c => c.Transactions!)
                   .WithOne(t => t.Category!)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.RecurringTransactions!)
                   .WithOne(rt => rt.Category!)
                   .HasForeignKey(rt => rt.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Categories!)
                   .HasForeignKey(c => c.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}


