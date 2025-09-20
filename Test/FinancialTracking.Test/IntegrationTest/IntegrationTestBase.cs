using AutoMapper;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using FinancialTracking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Test.IntegrationTest
{
    public class IntegrationTestBase
    {
        protected DbContextOptions<FTDbContext> _contextOptions { get; private set; }
        protected IMapper _mapper;
        public void SetContextOptions(DbContextOptions<FTDbContext> contextOptions)
        {
            _contextOptions = contextOptions;

            // Mapper setup
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDto>().ReverseMap();
                cfg.CreateMap<CreateTransactionRequest, Transaction>().ReverseMap();
                cfg.CreateMap<UpdateTransactionRequest, Transaction>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            Seed();
        }

        public void Seed()
        {
            using (var context = new FTDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //User
                var user = new User { Id = "user1", UserName = "Test User" };
                context.Users.Add(user);
                context.SaveChanges();

                // Categories ekle
                context.Categories.Add(new Category { Id = 1, Name = "Faturalar", UserId = "user1",Created = DateTime.Now });
                context.Categories.Add(new Category { Id = 2, Name = "Kira", UserId = "user1",Created = DateTime.Now });
                context.SaveChanges();


                // Transactions ekle
                context.Transactions.Add(new Transaction
                {
                    Id = 1,
                    Amount = new Money { Amount = 100, Currency = "TRY" },
                    CategoryId = 1,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Description = "Fatura",
                    Type = TransactionType.Expense,
                    UserId = "user1"
                });

                context.Transactions.Add(new Transaction
                {
                    Id = 2,
                    Amount = new Money { Amount = 15000, Currency = "TRY" },
                    CategoryId = 2,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Description = "Kira",
                    Type = TransactionType.Income,
                    UserId = "user1"
                });

                context.SaveChanges();
            }
        }

    }
}
