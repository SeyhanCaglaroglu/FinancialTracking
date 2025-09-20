using FinancialTracking.API.Controllers;
using FinancialTracking.Application.Features.Budgets.CommonDto;
using FinancialTracking.Application.Features.Budgets.Create;
using FinancialTracking.Application.Features.Budgets.Services;
using FinancialTracking.Application.Features.Budgets.Update;
using FinancialTracking.Application;
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
    public class BudgetsControllerTest
    {
        private readonly Mock<IBudgetService> _mockService;
        private readonly BudgetsController _controller;
        private List<BudgetDto> budgetDtos;

        public BudgetsControllerTest()
        {
            _mockService = new Mock<IBudgetService>();
            _controller = new BudgetsController(_mockService.Object);

            budgetDtos = new List<BudgetDto>()
            {
                new BudgetDto(new Money { Amount = 1000, Currency = "TRY" }, DateTime.Now, DateTime.Now),
                new BudgetDto(new Money { Amount = 2000, Currency = "TRY" }, DateTime.Now, DateTime.Now)
            };
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetAllBudgets_ActionExecutes_ReturnOk(string userId)
        {
            _mockService.Setup(x => x.GetAllListAsync(userId))
                .ReturnsAsync(new ServiceResult<List<BudgetDto>> { Data = budgetDtos, Status = HttpStatusCode.OK });

            var result = await _controller.GetAllBudgets(userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<List<BudgetDto>>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData(3, "user3")]
        public async Task GetBudgetById_InValidId_ReturnNotFound(int id, string userId)
        {
            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<BudgetDto> { Status = HttpStatusCode.NotFound });

            var result = await _controller.GetBudgetById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task GetBudgetById_ValidId_ReturnOk(int id, string userId)
        {
            BudgetDto dto = budgetDtos.First();

            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<BudgetDto> { Data = dto, Status = HttpStatusCode.OK });

            var result = await _controller.GetBudgetById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<BudgetDto>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateBudget_ActionExecutes_ReturnCreated(string userId)
        {
            CreateBudgetRequest request = new CreateBudgetRequest(new Money { Amount = 3000, Currency = "TRY" }, userId);
            CreateBudgetResponse response = new CreateBudgetResponse(Id: 1);

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateBudgetResponse>
                {
                    Status = HttpStatusCode.Created,
                    Data = response,
                    UrlAsCreated = "api/budgets/1"
                });

            var result = await _controller.CreateBudget(request);

            var redirect = Assert.IsType<CreatedResult>(result);
            Assert.IsAssignableFrom<ServiceResult<CreateBudgetResponse>>(redirect.Value);
            Assert.Equal(201, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateBudget_InvalidRequest_ReturnBadRequest(string userId)
        {
            // Örnek olarak zaten var olan veya hatalı bir bütçe isteği
            CreateBudgetRequest request = new CreateBudgetRequest(new Money { Amount = 0, Currency = "TRY" }, userId);

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateBudgetResponse>
                {
                    Status = HttpStatusCode.BadRequest
                });

            var result = await _controller.CreateBudget(request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, redirect.StatusCode);
        }


        [Theory]
        [InlineData(1)]
        public async Task UpdateBudget_ActionExecutes_ReturnNoContent(int id)
        {
            UpdateBudgetRequest request = new UpdateBudgetRequest(new Money { Amount = 3500, Currency = "TRY" }, "user1");

            _mockService.Setup(x => x.UpdateAsync(id, request))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.UpdateBudget(id, request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task DeleteBudget_ActionExecutes_ReturnNoContent(int id, string userId)
        {
            _mockService.Setup(x => x.DeleteAsync(id, userId))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.DeleteBudget(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }
    }
}
