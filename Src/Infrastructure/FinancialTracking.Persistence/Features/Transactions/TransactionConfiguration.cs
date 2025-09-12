using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Features.Transactions
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(t => t.Type)
                   .IsRequired();

            builder.Property(t => t.Created)
                   .IsRequired();

            builder.Property(t => t.Updated)
                   .IsRequired(false);

            // Money value object mapping
            builder.OwnsOne(t => t.Amount, moneyBuilder =>
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

            // Category (optional) relation
            builder.HasOne(t => t.Category)
                   .WithMany(c => c.Transactions!)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            // User (required) relation via BaseEntity
            builder.HasOne(t => t.User)
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.ClientCascade);

            
        }
    }
}
