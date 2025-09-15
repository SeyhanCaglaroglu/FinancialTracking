using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Budgets.CommonDto;
using FinancialTracking.Application.Features.Budgets.Create;
using FinancialTracking.Application.Features.Budgets.Services;
using FinancialTracking.Application.Features.Budgets.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Budgets")]
    [Authorize]
    public class BudgetsController(IBudgetService _budgetService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpGet("GetAllBudgets/{userId}", Name = "GetAllBudgets")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<BudgetDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<BudgetDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<BudgetDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<BudgetDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBudgets(string userId) => CreateActionResult(await _budgetService.GetAllListAsync(userId));

        [MapToApiVersion("1")]
        [HttpGet("GetBudgetById/{id}/{userId}", Name = "GetBudgetById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<BudgetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<BudgetDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<BudgetDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<BudgetDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<BudgetDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBudgetById(int id, string userId) => CreateActionResult(await _budgetService.GetByIdAsync(id, userId));

        [MapToApiVersion("1")]
        [HttpPost("CreateBudget",Name = "CreateBudget")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CreateBudgetResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CreateBudgetResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CreateBudgetResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CreateBudgetResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CreateBudgetResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBudget(CreateBudgetRequest request) => CreateActionResult(await _budgetService.CreateAsync(request));

        [MapToApiVersion("1")]
        [HttpPut("UpdateBudget/{id}", Name = "UpdateBudget")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBudget(int id, UpdateBudgetRequest request) => CreateActionResult(await _budgetService.UpdateAsync(id, request));

        [MapToApiVersion("1")]
        [HttpDelete("DeleteBudget/{id}/{userId}", Name = "DeleteBudget")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBudget(int id, string userId) => CreateActionResult(await _budgetService.DeleteAsync(id, userId));
    }
}
