using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Application.Features.Transactions.Create;
using FinancialTracking.Application.Features.Transactions.Services;
using FinancialTracking.Application.Features.Transactions.Update;
using FinancialTracking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Transactions")]
    [Authorize]
    public class TransactionsController(ITransactionService _transactionService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpGet("GetAllTransactions/{userId}", Name = "GetAllTransactions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactions(string userId) => CreateActionResult(await _transactionService.GetAllListAsync(userId));

        [MapToApiVersion("1")]
        [HttpGet("GetTransactionById/{id}/{userId}", Name = "GetTransactionById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<TransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<TransactionDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<TransactionDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<TransactionDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<TransactionDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionById(int id, string userId) => CreateActionResult(await _transactionService.GetByIdAsync(id, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetTransactionsByType/{transactionType}/{userId}", Name = "GetTransactionsByType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByType(TransactionType transactionType, string userId) => CreateActionResult(await _transactionService.GetTransactionsByTypeAsync(transactionType, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetTransactionsInCategoryByType/{transactionType}/{userId}", Name = "GetTransactionsInCategoryByType")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsInCategoryByType(TransactionType transactionType, string userId) => CreateActionResult(await _transactionService.GetTransactionsInCategoryByType(transactionType, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetTransactionsByCategory/{categoryId}/{userId}", Name = "GetTransactionsByCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsByCategory(int categoryId, string userId) => CreateActionResult(await _transactionService.GetTransactionsByCategoryAsync(categoryId, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetTransactionsInCategoryByCategoryId/{categoryId}/{userId}", Name = "GetTransactionsInCategoryByCategoryId")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<TransactionInCategoryDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionsInCategoryByCategoryId(int categoryId, string userId) => CreateActionResult(await _transactionService.GetTransactionsInCategoryByCategoryId(categoryId, userId));

        [MapToApiVersion("1")]
        [HttpPost(Name = "CreateTransaction")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CreateTransactionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CreateTransactionResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CreateTransactionResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CreateTransactionResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CreateTransactionResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTransaction(CreateTransactionRequest request) => CreateActionResult(await _transactionService.CreateAsync(request));

        [MapToApiVersion("1")]
        [HttpPut("UpdateTransaction/{id}", Name = "UpdateTransaction")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionRequest request) => CreateActionResult(await _transactionService.UpdateAsync(id, request));

        [MapToApiVersion("1")]
        [HttpDelete("DeleteTransaction/{id}/{userId}", Name = "DeleteTransaction")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTransaction(int id, string userId) => CreateActionResult(await _transactionService.DeleteAsync(id, userId));
    }
}
