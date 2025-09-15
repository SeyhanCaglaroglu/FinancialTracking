using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.RecurringTransactions.CommonDto;
using FinancialTracking.Application.Features.RecurringTransactions.Create;
using FinancialTracking.Application.Features.RecurringTransactions.Services;
using FinancialTracking.Application.Features.RecurringTransactions.Update;
using FinancialTracking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("RecurringTransactions")]
    [Authorize]
    public class RecurringTransactionsController(IRecurringTransactionService _recurringTransactionService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpGet("GetAllRecurringTransactions/{userId}",Name = "GetAllRecurringTransactions")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRecurringTransactions(string userId) => CreateActionResult(await _recurringTransactionService.GetAllListAsync(userId));

        [MapToApiVersion("1")]
        [HttpGet("GetRecurringTransactionById/{id}/{userId}", Name = "GetRecurringTransactionById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<RecurringTransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<RecurringTransactionDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<RecurringTransactionDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<RecurringTransactionDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<RecurringTransactionDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecurringTransactionById(int id, string userId) => CreateActionResult(await _recurringTransactionService.GetByIdAsync(id, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetRecurringTransactionsByType/{transactionType}/{userId}", Name = "GetRecurringTransactionsByType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecurringTransactionsByType(TransactionType transactionType,string userId) => CreateActionResult(await _recurringTransactionService.GetRecurringTransactionsByTypeAsync(transactionType, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetRecurringTransactionsInCategoryByType/{transactionType}/{userId}", Name = "GetRecurringTransactionsInCategoryByType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecurringTransactionsInCategoryByType(TransactionType transactionType, string userId) => CreateActionResult(await _recurringTransactionService.GetRecurringTransactionsInCategoryByTypeAsync(transactionType, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetRecurringTransactionsByCategory/{categoryId}/{userId}", Name = "GetRecurringTransactionsByCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecurringTransactionsByCategory(int categoryId, string userId) => CreateActionResult(await _recurringTransactionService.GetRecurringTransactionsByCategoryAsync(categoryId, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetRecurringTransactionsInCategoryByCategory/{categoryId}/{userId}", Name = "GetRecurringTransactionsInCategoryByCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<RecurringTransactionInCategoryDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecurringTransactionsInCategoryByCategory(int categoryId, string userId) => CreateActionResult(await _recurringTransactionService.GetRecurringTransactionsInCategoryByCategoryAsync(categoryId, userId));

        [MapToApiVersion("1")]
        [HttpPost("CreateRecurringTransaction",Name = "CreateRecurringTransaction")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CreateRecurringTransactionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CreateRecurringTransactionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CreateRecurringTransactionResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CreateRecurringTransactionResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CreateRecurringTransactionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRecurringTransaction(CreateRecurringTransactionRequest request) => CreateActionResult(await _recurringTransactionService.CreateAsync(request));

        [MapToApiVersion("1")]
        [HttpPut("UpdateRecurringTransaction/{id}", Name = "UpdateRecurringTransaction")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRecurringTransaction(int id,UpdateRecurringTransactionRequest request) => CreateActionResult(await _recurringTransactionService.UpdateAsync(id, request));

        [MapToApiVersion("1")]
        [HttpDelete("DeleteRecurringTransaction/{id}/{userId}", Name = "DeleteRecurringTransaction")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRecurringTransaction(int id, string userId) => CreateActionResult(await _recurringTransactionService.DeleteAsync(id, userId));
    }
}
