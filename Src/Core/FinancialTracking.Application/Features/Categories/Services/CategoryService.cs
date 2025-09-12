using AutoMapper;
using FinancialTracking.Application.Contracts.Persistence;
using FinancialTracking.Application.Features.Categories.CommonDto;
using FinancialTracking.Application.Features.Categories.Create;
using FinancialTracking.Application.Features.Categories.Update;
using FinancialTracking.Application.Features.Transactions.CommonDto;
using FinancialTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTracking.Application.Features.Categories.Services
{
    public class CategoryService(ICategoryRepository _categoryRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : ICategoryService
    {
        public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request,string userId)
        {
            var anyCategory = await _categoryRepository.AnyAsync(x => x.Name == request.Name, userId);

            if(anyCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Fail("Kategori ismi zaten var.", HttpStatusCode.BadRequest);
            }

            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),
                $"api/categories/{category.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id, userId);

            _categoryRepository.Delete(category!);

            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync(string userId)
        {
            var categories = await _categoryRepository.GetAllAsync(userId);

            var categoryAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id, string userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id,userId);

            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var categoryAsDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetCategoryByNameAsync(string name, string userId)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(name, userId);

            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("Kategori Bulunamadi.", HttpStatusCode.NotFound);
            }

            var categoryAsDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request, string userId)
        {
            var category = _mapper.Map<Category>(request);
            category.Id = id;
            category.UserId = userId;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
