using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTracking.Persistence.Features.RecurringTransactions
{
    public class RecurringTransactionConfiguration : IEntityTypeConfiguration<RecurringTransaction>
    {
        public void Configure(EntityTypeBuilder<RecurringTransaction> builder)
        {
            builder.ToTable("RecurringTransactions");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(rt => rt.Type)
                   .IsRequired();

            builder.Property(rt => rt.DayRepeatInterval)
                   .IsRequired();

            builder.Property(rt => rt.NextExecutionDate)
                   .IsRequired();

            builder.Property(rt => rt.Created)
                   .IsRequired();

            builder.Property(rt => rt.Updated)
                   .IsRequired(false);

            builder.OwnsOne(rt => rt.Amount, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                            .HasColumnName("Amount")
                            .HasPrecision(18, 2)
                            .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                            .HasColumnName("Currency")
                            .HasMaxLength(3)
                            .IsRequired();
            });

            builder.HasOne(rt => rt.Category)
                   .WithMany(c => c.RecurringTransactions!)
                   .HasForeignKey(rt => rt.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(rt => rt.User)
                   .WithMany()
                   .HasForeignKey(rt => rt.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}


