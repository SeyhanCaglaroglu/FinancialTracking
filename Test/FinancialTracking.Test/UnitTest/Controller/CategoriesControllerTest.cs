using FinancialTracking.API.Controllers;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Services;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application;
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
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryService> _mockService;
        private readonly CategoriesController _controller;
        private List<CategoryDto> categoryDtos;

        public CategoriesControllerTest()
        {
            _mockService = new Mock<ICategoryService>();
            _controller = new CategoriesController(_mockService.Object);

            categoryDtos = new List<CategoryDto>()
        {
            new CategoryDto(
                Id: 1,
                Name: "Gıda",
                Created: DateTime.Now,
                Updated: DateTime.Now,
                Transactions: new List<TransactionDto>(),
                RecurringTransactions: new List<RecurringTransactionDto>()
            ),
            new CategoryDto(
                Id: 2,
                Name: "Teknoloji",
                Created: DateTime.Now,
                Updated: DateTime.Now,
                Transactions: new List<TransactionDto>(),
                RecurringTransactions: new List<RecurringTransactionDto>()
            )
        };
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetAllCategories_ActionExecutes_ReturnOk(string userId)
        {
            _mockService.Setup(x => x.GetAllListAsync(userId))
                .ReturnsAsync(new ServiceResult<List<CategoryDto>> { Data = categoryDtos, Status = HttpStatusCode.OK });

            var result = await _controller.GetAllCategories(userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.IsAssignableFrom<ServiceResult<List<CategoryDto>>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData(3, "user3")]
        public async Task GetCategoryById_InValidId_ReturnNotFound(int id, string userId)
        {
            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<CategoryDto> { Status = HttpStatusCode.NotFound });

            var result = await _controller.GetCategoryById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task GetCategoryById_ValidId_ReturnOk(int id, string userId)
        {
            CategoryDto dto = categoryDtos.First();

            _mockService.Setup(x => x.GetByIdAsync(id, userId))
                .ReturnsAsync(new ServiceResult<CategoryDto> { Data = dto, Status = HttpStatusCode.OK });

            var result = await _controller.GetCategoryById(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<CategoryDto>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData("Elektronik", "user3")]
        public async Task GetCategoryByName_InValidName_ReturnNotFound(string name, string userId)
        {
            _mockService.Setup(x => x.GetCategoryByNameAsync(name, userId))
                .ReturnsAsync(new ServiceResult<CategoryDto> { Status = HttpStatusCode.NotFound });

            var result = await _controller.GetCategoryByName(name, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, redirect.StatusCode);
        }

        [Theory]
        [InlineData("Teknoloji", "user1")]
        public async Task GetCategoryByName_ValidName_ReturnOk(string name, string userId)
        {
            CategoryDto dto = categoryDtos.Last();

            _mockService.Setup(x => x.GetCategoryByNameAsync(name, userId))
                .ReturnsAsync(new ServiceResult<CategoryDto> { Data = dto, Status = HttpStatusCode.OK });

            var result = await _controller.GetCategoryByName(name, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.IsAssignableFrom<ServiceResult<CategoryDto>>(redirect.Value);
            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateCategory_ExistingName_ReturnBadRequest(string userId)
        {
            CreateCategoryRequest request = new CreateCategoryRequest(Name: "Teknoloji", userId: userId);

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateCategoryResponse> { Status = HttpStatusCode.BadRequest });

            var result = await _controller.CreateCategory(request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateCategory_UniqueName_ReturnCreated(string userId)
        {
            CreateCategoryRequest request = new CreateCategoryRequest(Name: "Spor", userId: userId);
            CreateCategoryResponse response = new CreateCategoryResponse(Id: 3);

            _mockService.Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new ServiceResult<CreateCategoryResponse>
                {
                    Status = HttpStatusCode.Created,
                    Data = response,
                    UrlAsCreated = "api/categories/3"
                });

            var result = await _controller.CreateCategory(request);

            var redirect = Assert.IsType<CreatedResult>(result);
            Assert.IsAssignableFrom<ServiceResult<CreateCategoryResponse>>(redirect.Value);
            Assert.Equal(201, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateCategory_ActionExecutes_ReturnNoContent(int id)
        {
            UpdateCategoryRequest request = new UpdateCategoryRequest(Name: "Yeni Gıda", userId: "user1");

            _mockService.Setup(x => x.UpdateAsync(id, request))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.UpdateCategory(id, request);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task DeleteCategory_ActionExecutes_ReturnNoContent(int id, string userId)
        {
            _mockService.Setup(x => x.DeleteAsync(id, userId))
                .ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.DeleteCategory(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);
            Assert.Equal(204, redirect.StatusCode);
        }
    }
}
