using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTracking.Persistence.Features.Budgets
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("Budgets");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Created)
                   .IsRequired();

            builder.Property(b => b.Updated)
                   .IsRequired(false);

            // Money value object mapping
            builder.OwnsOne(b => b.TotalAmount, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                            .HasColumnName("TotalAmount")
                            .HasPrecision(18, 2)
                            .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                            .HasColumnName("Currency")
                            .HasMaxLength(3)
                            .IsRequired();
            });

            // One-to-one relation with User
            builder.HasOne(b => b.User)
                   .WithOne(u => u.Budget!)
                   .HasForeignKey<Budget>(b => b.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => b.UserId)
                   .IsUnique();
        }
    }
}


