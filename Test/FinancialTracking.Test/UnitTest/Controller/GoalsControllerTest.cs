using FinancialTracking.API.Controllers;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Goals.CommonDto;
using FinancialTracking.Application.Features.Goals.Create;
using FinancialTracking.Application.Features.Goals.Services;
using FinancialTracking.Application.Features.Goals.Update;
using FinancialTracking.Domain.Entities;
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
    public class GoalsControllerTest
    {
        private readonly Mock<IGoalService> _mockService;
        private readonly GoalsController _controller;
        private List<GoalDto> goalDtos;

        public GoalsControllerTest()
        {
            _mockService = new Mock<IGoalService>();
            _controller = new GoalsController(_mockService.Object);

            var targetAmount = new Money
            {
                Amount = 20000,
                Currency = "TRY"
            };

            var currentAmount = new Money
            {
                Amount = 5000,
                Currency = "TRY"
            };

            goalDtos = new List<GoalDto>() 
            {
                new GoalDto(
                        Title:"Bilgisayar almak",
                        TargetAmount: targetAmount,
                        CurrentAmount: currentAmount,
                        Deadline:DateTime.Now.AddDays(20),
                        Created:DateTime.Now,
                        Updated:DateTime.Now
                    ),
                new GoalDto(
                        Title:"Telefon almak",
                        TargetAmount: targetAmount,
                        CurrentAmount: currentAmount,
                        Deadline:DateTime.Now.AddDays(40),
                        Created:DateTime.Now,
                        Updated:DateTime.Now
                    )
            };
        }

        [Theory]
        [InlineData("user1")]
        public async Task GetAllGoals_ActionExecutes_ReturnOk(string userId)
        {
            _mockService.Setup(x => x.GetAllListAsync(userId)).ReturnsAsync(new ServiceResult<List<GoalDto>> { Data=goalDtos,Status=HttpStatusCode.OK});

            var result = await _controller.GetAllGoals(userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.IsAssignableFrom<ServiceResult<List<GoalDto>>>(redirect.Value);

            Assert.Equal(200,redirect.StatusCode);
        }

        [Theory]
        [InlineData(3,"user3")]
        public async Task GetGoalById_InValidId_ReturnNotFound(int id,string userId)
        {
            _mockService.Setup(x=>x.GetByIdAsync(id,userId)).ReturnsAsync(new ServiceResult<GoalDto> { Status=HttpStatusCode.NotFound });

            var result = await _controller.GetGoalById(id, userId);

            var redirect = Assert.IsType<ObjectResult> (result);

            Assert.Equal(404,redirect.StatusCode);
        }

        [Theory]
        [InlineData(1,"user1")]
        public async Task GetGoalById_ValidId_ReturnOk(int id, string userId)
        {
            GoalDto goalDto = goalDtos.First();

            _mockService.Setup(x => x.GetByIdAsync(id, userId)).ReturnsAsync(new ServiceResult<GoalDto> { Data = goalDto, Status = HttpStatusCode.OK });

            var result = await _controller.GetGoalById(id,userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.IsAssignableFrom<ServiceResult<GoalDto>> (redirect.Value);

            Assert.Equal(200,redirect.StatusCode);
        }

        [Theory]
        [InlineData("TestTitle","user3")]
        public async Task GetGoalByTitle_InValidTitle_ReturnNotFound(string title, string userId)
        {
            _mockService.Setup(x => x.GetGoalByTitleAsync(title, userId)).ReturnsAsync(new ServiceResult<GoalDto> { Status = HttpStatusCode.NotFound });

            var result = await _controller.GetGoalByTitle(title, userId);

            var redirect = Assert.IsType<ObjectResult> (result);

            Assert.Equal(404,redirect.StatusCode);
        }

        [Theory]
        [InlineData("Bilgisayar almak", "user1")]
        public async Task GetGoalByTitle_ValidTitle_ReturnOk(string title, string userId)
        {
            GoalDto goalDto = goalDtos.First();

            _mockService.Setup(x => x.GetGoalByTitleAsync(title, userId)).ReturnsAsync(new ServiceResult<GoalDto> {Data=goalDto, Status = HttpStatusCode.OK });

            var result = await _controller.GetGoalByTitle(title, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.IsAssignableFrom<ServiceResult<GoalDto>> (redirect.Value);

            Assert.Equal(200, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateGoal_ExistingTitle_ReturnBadRequest(string userId)
        {
            CreateGoalRequest createGoalRequest = new CreateGoalRequest(
                    Title: "Bilgisayar almak",
                    TargetAmount: new Money { Amount=20000,Currency="TRY"},
                    CurrentAmount: new Money { Amount = 5000, Currency = "TRY" },
                    Deadline: DateTime.Now.AddDays(20),
                    userId: userId
                );

            _mockService.Setup(x=>x.CreateAsync(createGoalRequest)).ReturnsAsync(new ServiceResult<CreateGoalResponse> {Status = HttpStatusCode.BadRequest});

            var result = await _controller.CreateGoal(createGoalRequest);

            var redirect = Assert.IsType<ObjectResult> (result);

            Assert.Equal(400, redirect.StatusCode);
        }

        [Theory]
        [InlineData("user1")]
        public async Task CreateGoal_UniqueTitle_ReturnCreated(string userId)
        {
            CreateGoalRequest createGoalRequest = new CreateGoalRequest(
                    Title: "Canta almak",
                    TargetAmount: new Money { Amount = 3000, Currency = "TRY" },
                    CurrentAmount: new Money { Amount = 10, Currency = "TRY" },
                    Deadline: DateTime.Now.AddDays(30),
                    userId: userId
                );
            CreateGoalResponse createGoalResponse = new CreateGoalResponse(Id: 3);

            _mockService.Setup(x => x.CreateAsync(createGoalRequest)).ReturnsAsync(new ServiceResult<CreateGoalResponse> { Status = HttpStatusCode.Created,Data= createGoalResponse,UrlAsCreated= "api/goals/3" });

            var result = await _controller.CreateGoal(createGoalRequest);

            var redirect = Assert.IsType<CreatedResult>(result);

            Assert.IsAssignableFrom<ServiceResult<CreateGoalResponse>>(redirect.Value);

            Assert.Equal(201, redirect.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateGoal_ActionExecutes_ReturnNoContent(int id)
        {
            UpdateGoalRequest updateGoalRequest = new UpdateGoalRequest(
                    Title: "Bilgisayar almak",
                    TargetAmount: new Money { Amount = 22000, Currency = "TRY" },
                    CurrentAmount: new Money { Amount = 5000, Currency = "TRY" },
                    Deadline: DateTime.Now.AddDays(20),
                    userId: "user1"
                );

            _mockService.Setup(x => x.UpdateAsync(id, updateGoalRequest)).ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.UpdateGoal(id, updateGoalRequest);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204,redirect.StatusCode);
        }

        [Theory]
        [InlineData(1, "user1")]
        public async Task DeleteGoal_ActionExecutes_ReturnNoContent(int id, string userId)
        {
            _mockService.Setup(x => x.DeleteAsync(id, userId)).ReturnsAsync(new ServiceResult { Status = HttpStatusCode.NoContent });

            var result = await _controller.DeleteGoal(id, userId);

            var redirect = Assert.IsType<ObjectResult>(result);

            Assert.Equal(204, redirect.StatusCode);
        }
    }
}
