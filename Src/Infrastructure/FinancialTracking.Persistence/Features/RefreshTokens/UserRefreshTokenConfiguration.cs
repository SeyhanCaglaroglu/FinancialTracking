using FinancialTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTracking.Persistence.Features.RefreshTokens
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(200);
        }
    }
}


