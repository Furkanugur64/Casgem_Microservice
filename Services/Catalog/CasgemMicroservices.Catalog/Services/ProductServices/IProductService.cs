using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Catalog.DTOs.ProductDTOs;

namespace CasgemMicroservices.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDTO>>> GetProductListAsync();
        Task<Response<List<ResultProductDTO>>> GetProductListWithCategoryAsync();
        Task<Response<ResultProductDTO>> GetProductByIdAsync(string id);

        Task<Response<CreateProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO);
        Task<Response<UpdateProductDTO>> UpdateProductAsync(UpdateProductDTO updateProductDTO);
        Task<Response<NoContent>> DeleteProductAsync(string id);
    }
}
