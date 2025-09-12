using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTracking.Persistence.Features.Goals
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.ToTable("Goals");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(g => g.Deadline)
                   .IsRequired();

            builder.Property(g => g.Created)
                   .IsRequired();

            builder.Property(g => g.Updated)
                   .IsRequired(false);

            builder.OwnsOne(g => g.TargetAmount, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                            .HasColumnName("TargetAmount")
                            .HasPrecision(18, 2)
                            .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                            .HasColumnName("Currency")
                            .HasMaxLength(3)
                            .IsRequired();
            });

            builder.OwnsOne(g => g.CurrentAmount, moneyBuilder =>
            {
                moneyBuilder.Property(m => m.Amount)
                            .HasColumnName("CurrentAmount")
                            .HasPrecision(18, 2)
                            .IsRequired();

                moneyBuilder.Property(m => m.Currency)
                            .HasColumnName("Currency")
                            .HasMaxLength(3)
                            .IsRequired();
            });

            builder.HasOne(g => g.User)
                   .WithMany(u => u.Goals!)
                   .HasForeignKey(g => g.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}


