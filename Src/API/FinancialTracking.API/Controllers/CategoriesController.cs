using Asp.Versioning;
using FinancialTracking.Application;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Services;
using FinancialTracking.Application.Features.Categories.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTracking.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Tags("Categories")]
    [Authorize]
    public class CategoriesController(ICategoryService _categoryService) : CustomBaseController
    {
        [MapToApiVersion("1")]
        [HttpGet("GetAllCategories/{userId}", Name = "GetAllCategories")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<List<CategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<List<CategoryDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<List<CategoryDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<List<CategoryDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCategories(string userId) => CreateActionResult(await _categoryService.GetAllListAsync(userId));

        [MapToApiVersion("1")]
        [HttpGet("GetCategoryById/{id}/{userId}", Name = "GetCategoryById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryById(int id, string userId) => CreateActionResult(await _categoryService.GetByIdAsync(id, userId));

        [MapToApiVersion("1")]
        [HttpGet("GetCategoryByName/{name}/{userId}", Name = "GetCategoryByName")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceResult<CategoryDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryByName(string name,string userId) => CreateActionResult(await _categoryService.GetCategoryByNameAsync(name, userId));

        [MapToApiVersion("1")]
        [HttpPost("CreateCategory",Name = "CreateCategory")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult<CreateCategoryResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceResult<CreateCategoryResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CreateCategoryResponse>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult<CreateCategoryResponse>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult<CreateCategoryResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) => CreateActionResult(await _categoryService.CreateAsync(request));

        [MapToApiVersion("1")]
        [HttpPut("UpdateCategory/{id}", Name = "UpdateCategory")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) => CreateActionResult(await _categoryService.UpdateAsync(id, request));

        [MapToApiVersion("1")]
        [HttpDelete("DeleteCategory/{id}/{userId}", Name = "DeleteCategory")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int id, string userId) => CreateActionResult(await _categoryService.DeleteAsync(id, userId));
    }
}
