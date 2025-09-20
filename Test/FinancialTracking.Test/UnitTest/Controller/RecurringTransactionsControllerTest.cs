using FinancialTracking.API.Controllers;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.RecurringTransactions.Services;
using FinancialTracking.Application.Features.RecurringTransactions.Update;
using FinancialTracking.Application;
using FinancialTracking.Domain.Entities;
using FinancialTracking.Domain.Enums;
using FinancialTracking.Domain.ValueObjects;
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
    public class RecurringTransactionsControllerTest
    {
        private readonly Mock<IRecurringTransactionService> _mockService;
        private readonly RecurringTransactionsController _controller;
        private List<RecurringTransactionDto> recurringTransactionDtos;
        

        public RecurringTransactionsControllerTest()
        {
            _mockService = new Mock<IRecurringTransactionService>();
            _controller = new RecurringTransactionsController(_mockService.Object);

            recurringTransactionDtos = new List<RecurringTransactionDto>()
        {
            new RecurringTransactionDto(
                Id: 1,
                Amount: new Money { Amount = 500, Currency = "TRY" },
                Description: "Kira",
                Type: TransactionType.Expense,
                NextExecutionDate: DateTime.Now.AddDays(5),
                DayRepeatInterval: 30,
                CategoryId: 1,
                Created: DateTime.Now,
                Updated: DateTime.Now
            ),
            new RecurringTransactionDto(
                Id: 2,
                Amount: new Money { Amount = 1000, Currency = "TRY" },
                Description: "Maaş",
                Type: TransactionType.Income,
                NextExecutionDate: DateTime.Now.AddDays(1),
                DayRepeatInterval: 30,
                CategoryId: 2,
                Created: DateTime.Now,
                Updated: DateTime.Now
            )
        };

            
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetAllRecurringTransactions_ActionExecutes_ReturnOk(string userId)
        {
            _mockService.Setup(x => x.GetAllListAsync(userId))
                .ReturnsAsync(new ServiceResult<List<RecurringTransactionDto>> { Data = recurringTransactionDtos, Status = HttpStatusCode.OK });

            var result = await _controller.GetAllRecurringTransactions(userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<List<RecurringTransactionDto>>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData(3, "user3")]
        public async Task GetRecurringTransactionById_InvalidId_ReturnNotFound(int id, string userId)
        {
            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<RecurringTransactionDto> { Status = HttpStatusCode.NotFound });

            var result = await _controller.GetRecurringTransactionById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task GetRecurringTransactionById_ValidId_ReturnOk(int id, string userId)
        {
            RecurringTransactionDto dto = recurringTransactionDtos.First();

            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<RecurringTransactionDto> { Data = dto, Status = HttpStatusCode.OK });

            var result = await _controller.GetRecurringTransactionById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<RecurringTransactionDto>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData(TransactionType.Income, "user1")]
        public async Task GetRecurringTransactionsByType_ActionExecutes_ReturnOk(TransactionType type, string userId)
        {
            _mockService.Setup(x => x.GetRecurringTransactionsByTypeAsync(type, userId))
                .ReturnsAsync(new ServiceResult<List<RecurringTransactionDto>> { Data = recurringTransactionDtos, Status = HttpStatusCode.OK });

            var result = await _controller.GetRecurringTransactionsByType(type, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<List<RecurringTransactionDto>>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        // Create, Update ve Delete için örnekler
        [Theory]
        [InlineData("user1")]
        public async Task CreateRecurringTransaction_ActionExecutes_ReturnCreated(string userId)
        {
            CreateRecurringTransactionRequest request = new CreateRecurringTransactionRequest(
                new Money { Amount = 1000, Currency = "TRY" },
                Description: "Maaş",
                Type: TransactionType.Income,
                NextExecutionDate: DateTime.Now.AddDays(1),
                DayRepeatInterval: 30,
                CategoryId: 2,
                userId: userId
            );
            CreateRecurringTransactionResponse response = new CreateRecurringTransactionResponse(Id: 1);

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateRecurringTransactionResponse>
                {
                    Status = HttpStatusCode.Created,
                    Data = response,
                    UrlAsCreated = "api/recurringtransactions/1"
                });

            var result = await _controller.CreateRecurringTransaction(request);

            var redirect = Assert.IsType<CreatedResult>(result);
            Assert.IsAssignableFrom<ServiceResult<CreateRecurringTransactionResponse>>(redirect.Value);
            Assert.Equal(201, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateRecurringTransaction_InvalidRequest_ReturnBadRequest(string userId)
        {
            CreateRecurringTransactionRequest request = new CreateRecurringTransactionRequest(
                new Money { Amount = 0, Currency = "TRY" },
                Description: "",
                Type: TransactionType.Income,
                NextExecutionDate: DateTime.Now,
                DayRepeatInterval: 0,
                CategoryId: null,
                userId: userId
            );

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateRecurringTransactionResponse> { Status = HttpStatusCode.BadRequest });

            var result = await _controller.CreateRecurringTransaction(request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateRecurringTransaction_ActionExecutes_ReturnNoContent(int id)
        {
            UpdateRecurringTransactionRequest request = new UpdateRecurringTransactionRequest(
                new Money { Amount = 1200, Currency = "TRY" },
                Description: "Kira",
                Type: TransactionType.Expense,
                NextExecutionDate: DateTime.Now.AddDays(5),
                DayRepeatInterval: 30,
                CategoryId: 1,
                userId: "user1"
            );

            _mockService.Setup(x => x.UpdateAsync(id, request))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.UpdateRecurringTransaction(id, request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task DeleteRecurringTransaction_ActionExecutes_ReturnNoContent(int id, string userId)
        {
            _mockService.Setup(x => x.DeleteAsync(id, userId))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.DeleteRecurringTransaction(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }
    }

}
