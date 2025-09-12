using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;


namespace FinancialTracking.Application.Features.Categories.Services
{
    public interface ICategoryService
    {
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync(string userId);
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id, string userId);
        Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request,string userId);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request, string userId);
        Task<ServiceResult> DeleteAsync(int id, string userId);
        Task<ServiceResult<CategoryDto>> GetCategoryByNameAsync(string name,string userId);
    }
}
