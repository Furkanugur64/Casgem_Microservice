using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Catalog.DTOs.CategoryDTOs;

namespace CasgemMicroservices.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<ResultCategoryDTO>>> GetCategoryListAsync();

        Task<Response<ResultCategoryDTO>> GetCategoryByIdAsync(string id);

        Task<Response<CreateCategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<Response<UpdateCategoryDTO>> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task<Response<NoContent>> DeleteCategoryAsync(string id);
    }
}
