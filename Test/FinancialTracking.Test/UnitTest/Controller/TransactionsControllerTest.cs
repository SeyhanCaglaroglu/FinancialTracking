using FinancialTracking.API.Controllers;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Services;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Test.UnitTest.Controller
{
    public class TransactionsControllerTest
    {
        private readonly Mock<ITransactionService> _mockService;
        private readonly TransactionsController _controller;
        private List<TransactionDto> transactions;

        public TransactionsControllerTest()
        {
            _mockService = new Mock<ITransactionService>();
            _controller = new TransactionsController(_mockService.Object);

            var money = new Money
            {
                Amount = 100,
                Currency = "TRY"
            };

            transactions = new List<TransactionDto>()
            {
                new TransactionDto(
                    Id: 1,
                    Amount: money,
                    Description: "Örnek işlem",
                    Type: TransactionType.Income,
                    Created: DateTime.Now,
                    Updated: DateTime.Now,
                    CategoryId: 1
                ),
                new TransactionDto(
                    Id: 2,
                    Amount: new Money { Amount = 250m, Currency = "TRY" },
                    Description: "Örnek işlem 2",
                    Type: TransactionType.Expense,
                    Created: DateTime.Now,
                    Updated: DateTime.Now,
                    CategoryId: 2
                )
            };
        }

        [Fact]
        public async Task GetAllTransactions_ActionExecutes_ReturnOk()
        {
            _mockService.Setup(x => x.GetAllListAsync("user1")).ReturnsAsync(new ServiceResult<List<TransactionDto>> { Data = transactions, Status = HttpStatusCode.OK });

            var result = await _controller.GetAllTransactions("user1");

            var okResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            Assert.IsAssignableFrom<ServiceResult<List<TransactionDto>>>(okResult.Value);
        }

        [Fact]
        public async Task GetTransactionById_ValidId_ReturnOk()
        {
            var transaction = transactions.First();

            _mockService.Setup(x => x.GetByIdAsync(1, "user1")).ReturnsAsync(new ServiceResult<TransactionDto> { Data = transaction, Status = HttpStatusCode.OK });

            var result = await _controller.GetTransactionById(1, "user1");

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)(HttpStatusCode.OK), redirect.StatusCode);

            Assert.IsAssignableFrom<ServiceResult<TransactionDto>>(redirect.Value);
        }

        [Fact]
        public async Task GetTransactionById_ValidId_ReturnNotFound()
        {
            _mockService.Setup(x => x.GetByIdAsync(3, "user3")).ReturnsAsync(new ServiceResult<TransactionDto> { Data = null, Status = HttpStatusCode.NotFound });

            var result = await _controller.GetTransactionById(3, "user3");

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(404, redirect.StatusCode);
        }

        [Fact]
        public async Task GetTransactionsByType_ActionsExecutes_ReturnOk()
        {
            _mockService.Setup(x => x.GetTransactionsByTypeAsync(TransactionType.Income, "user1")).ReturnsAsync(new ServiceResult<List<TransactionDto>> { Data = transactions, Status = HttpStatusCode.OK });

            var result = await _controller.GetTransactionsByType(TransactionType.Income, "user1");

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, redirect.StatusCode);

            Assert.IsAssignableFrom<ServiceResult<List<TransactionDto>>>(redirect.Value);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task GetTransactionsByCategory_ActionExecute_ReturnOk(int categoryId, string userId)
        {
            _mockService.Setup(x => x.GetTransactionsByCategoryAsync(categoryId, userId)).ReturnsAsync(new ServiceResult<List<TransactionDto>> { Data = transactions, Status = HttpStatusCode.OK });

            var result = await _controller.GetTransactionsByCategory(categoryId, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(200, redirect.StatusCode);

            Assert.IsAssignableFrom<ServiceResult<List<TransactionDto>>>(redirect.Value);
        }

        [Fact]
        public async Task CreateTransaction_ActionExecutes_ReturnCreated()
        {
            var request = new CreateTransactionRequest(
                new Money(100, "TRY"),       // Amount
                "Test İşlem",                // Description
                TransactionType.Income,      // Type
                1,                           // CategoryId
                "user1"               // userId
            );

            _mockService.Setup(x => x.CreateAsync(request)).ReturnsAsync(new ServiceResult<CreateTransactionResponse> { Status = HttpStatusCode.Created, UrlAsCreated = "api/transactions/1" });

            var result = await _controller.CreateTransaction(request);

            var redirect = Assert.IsType<CreatedResult>(result);

            Assert.Equal(201, redirect.StatusCode);

            Assert.IsAssignableFrom<ServiceResult<CreateTransactionResponse>>(redirect.Value);


        }

        [Fact]
        public async Task UpdateTransaction_ActionExecutes_ReturnNoContent()
        {
            // Arrange
            var request = new UpdateTransactionRequest(
                new Money(100, "TRY"),
                "Test İşlem",
                TransactionType.Income,
                1,
                "user1"
            );

            _mockService.Setup(x => x.UpdateAsync(1, request)).ReturnsAsync(new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            });

            // Act
            var result = await _controller.UpdateTransaction(1, request);

            // Assert
            var noContentResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task DeleteTransaction_ValidRequest_ReturnsNoContent()
        {
            // Arrange

            _mockService
                .Setup(x => x.DeleteAsync(1, "user1"))
                .ReturnsAsync(new ServiceResult { Status=HttpStatusCode.NoContent});

            // Act
            var result = await _controller.DeleteTransaction(1,"user1");

            // Assert
            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, redirect.StatusCode);

        }


    }
}
