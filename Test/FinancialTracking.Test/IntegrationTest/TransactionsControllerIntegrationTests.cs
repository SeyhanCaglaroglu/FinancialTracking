using FinancialTracking.API.Controllers;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Transactions;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Services;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using FinancialTracking.Persistence;
using FinancialTracking.Persistence.Context;
using FinancialTracking.Persistence.Features.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Test.IntegrationTest
{
    public class TransactionsControllerIntegrationTests : IntegrationTestBase
    {
        private readonly TransactionsController _controller;
        private readonly FTDbContext _context;

        public TransactionsControllerIntegrationTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            SetContextOptions(new DbContextOptionsBuilder<FTDbContext>().UseSqlite(connection).Options);

            // Context'i class alanında tut
            _context = new FTDbContext(_contextOptions);

            var repo = new TransactionRepository(_context);
            var uow = new UnitOfWork(_context);
            var service = new TransactionService(repo, uow, _mapper);

            _controller = new TransactionsController(service);
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetAllTransactions_ReturnTransactions(string userId)
        {
            var transactionList = _context.Transactions.ToList();

            var result = await _controller.GetAllTransactions(userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            var serviceResult = Assert.IsType<ServiceResult<List<TransactionDto>>>(redirect.Value);

            Assert.NotNull(serviceResult.Data);
            Assert.Equal(transactionList.Count, serviceResult.Data.Count);
        }

        [Theory]
        [InlineData(1,"user1")]
        public async Task GetTransactionById_ReturnTransaction(int id, string userId)
        {
            var transaction = _context.Transactions.Find(id);

            var result = await _controller.GetTransactionById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            var serviceResult = Assert.IsType<ServiceResult<TransactionDto>>(redirect.Value);

            Assert.NotNull(transaction);
            Assert.NotNull(serviceResult.Data);
            Assert.Equal(transaction.Id, serviceResult.Data.Id);
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetTransactionsByType_ReturnTransactions(string userId)
        {
            var transactionList = _context.Transactions.Where(x => x.Type == TransactionType.Income && x.UserId == userId).ToList();

            var result = await _controller.GetTransactionsByType(TransactionType.Income, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            var serviceResult = Assert.IsType<ServiceResult<List<TransactionDto>>>(redirect.Value);

            Assert.NotNull (serviceResult.Data);
            Assert.Equal(transactionList.Count, serviceResult.Data.Count);
        }

        [Theory]
        [InlineData(1,"user1")]
        public async Task GetTransactionsByCategory_ReturnTransactions(int id,string userId)
        {
            var transactionList = _context.Transactions.Where(x=>x.CategoryId == id && x.UserId == userId).ToList();

            var result = await _controller.GetTransactionsByCategory(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            var serviceResult = Assert.IsType<ServiceResult<List<TransactionDto>>> (redirect.Value);

            Assert.NotNull(serviceResult.Data);
            Assert.Equal(transactionList.Count,serviceResult.Data.Count);
        }

        [Fact]
        public async Task CreateTransaction_ReturnCreated()
        {
            var request = new CreateTransactionRequest(
                 new Money(100, "TRY"),       // Amount
                 "Test Create",                // Description
                 TransactionType.Income,      // Type
                 1,                           // CategoryId
                 "user1"               // userId
             );

            var result = await _controller.CreateTransaction(request);

            var redirect = Assert.IsType<CreatedResult>(result);

            var transaction = _context.Transactions.FirstOrDefault(x=>x.Description == request.Description);

            Assert.NotNull(transaction);

            Assert.Equal(request.Description,transaction.Description);

        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateTransaction_ReturnNoContent(int id)
        {
            var request = new UpdateTransactionRequest(
                new Money(200, "TRY"),
                "Update İşlem",
                TransactionType.Income,
                1,
                "user1"
                
            );

            var result = await _controller.UpdateTransaction(id, request);

            var transaction = _context.Transactions.FirstOrDefault(x=>x.Id == id);

            Assert.Equal(request.Description, transaction.Description);
            Assert.Equal(request.Amount.Amount, transaction.Amount.Amount);
        }

        [Theory]
        [InlineData(1,"user1")]
        public async Task DeleteTransaction_ReturnNoContent(int id, string userId)
        {
            var result = await _controller.DeleteTransaction(id, userId);

            var transaction = _context.Transactions.FirstOrDefault(x=>x.Id == id);

            Assert.Null(transaction);
        }
    }

}
