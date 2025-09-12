using FinancialTracking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Persistence.Context
{
    public class FTDbContext : IdentityDbContext<User,IdentityRole,string>
    {
        public FTDbContext(DbContextOptions<FTDbContext> options) : base(options)
        {

        }
        public DbSet<Transaction> Transactions { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Budget> Budgets { get; set; } = default!;
        public DbSet<Goal> Goals { get; set; } = default!;
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; } = default!;

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(FTDbContext).Assembly);
        }
    }
}
