using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Goals.CommonDto;
using FinancialTracking.Application.Features.Goals.Create;
using FinancialTracking.Application.Features.Goals.Services;
using FinancialTracking.Application.Features.Goals.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Goals")]
    [Authorize]
    public class GoalsController(IGoalService _goalService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpGet("GetAllGoals",Name = "GetAllGoals")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<GoalDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<GoalDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<GoalDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<GoalDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllGoals(string userId) => CreateActionResult(await _goalService.GetAllListAsync(userId));

        [MapToApiVersion("1")]
        [HttpGet("GetGoalById/{id}", Name = "GetGoalById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGoalById(int id,string userId) => CreateActionResult(await _goalService.GetByIdAsync(id, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetGoalByTitle/{title}", Name = "GetGoalByTitle")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<GoalDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGoalByTitle(string title,string userId) => CreateActionResult(await _goalService.GetGoalByTitleAsync(title, userId));

        [MapToApiVersion("1")]
        [HttpPost("CreateGoal",Name = "CreateGoal")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CreateGoalResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CreateGoalResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CreateGoalResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CreateGoalResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CreateGoalResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGoal(CreateGoalRequest request) => CreateActionResult(await _goalService.CreateAsync(request));

        [MapToApiVersion("1")]
        [HttpPut("UpdateGoal/{id}", Name = "UpdateGoal")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateGoal(int id, UpdateGoalRequest request) => CreateActionResult(await _goalService.UpdateAsync(id, request));

        [MapToApiVersion("1")]
        [HttpDelete("DeleteGoal/{id}", Name = "DeleteGoal")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGoal(int id, string userId) => CreateActionResult(await _goalService.DeleteAsync(id, userId));
    }
}
