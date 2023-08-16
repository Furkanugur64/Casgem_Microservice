using AutoMapper;
using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Catalog.DTOs.CategoryDTOs;
using CasgemMicroservices.Catalog.DTOs.ProductDTOs;
using CasgemMicroservices.Catalog.Models;
using CasgemMicroservices.Catalog.Settings.Abstract;
using MongoDB.Driver;

namespace CasgemMicroservices.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CreateProductDTO>> CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var value = _mapper.Map<Product>(createProductDTO);
            await _productCollection.InsertOneAsync(value);
            return Response<CreateProductDTO>.Success(_mapper.Map<CreateProductDTO>(value), 200);
        }

        public async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
            var value = await _productCollection.DeleteOneAsync(id);
            if (value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek Ürün Bulunamadı", 404);
            }
        }

        public async Task<Response<ResultProductDTO>> GetProductByIdAsync(string id)
        {
            var value = await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return Response<ResultProductDTO>.Fail("Böyle Bir ID Bulunamadı", 404);
            }
            else
            {
                return Response<ResultProductDTO>.Success(_mapper.Map<ResultProductDTO>(value), 200);
            }
        }

        public async Task<Response<List<ResultProductDTO>>> GetProductListAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDTO>>.Success(_mapper.Map<List<ResultProductDTO>>(values), 200);
        }

        public async Task<Response<List<ResultProductDTO>>> GetProductListWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            if (values.Any())
            {
                foreach (var item in values)
                {
                    item.Category=await _categoryCollection.Find(x=>x.CategoryID==item.CategoryID).FirstOrDefaultAsync();
                }
            }
            else
            {
                values =new List<Product> { };
            }
            return Response<List<ResultProductDTO>>.Success(_mapper.Map<List<ResultProductDTO>>(values), 200);
        }

        public async Task<Response<UpdateProductDTO>> UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            var value = _mapper.Map<Product>(updateProductDTO);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDTO.ProductID, value);
            if (result == null)
            {
                return Response<UpdateProductDTO>.Fail("Güncellenecek Veri Bulunamadı", 404);
            }
            else
            {
                return Response<UpdateProductDTO>.Success(204);
            }
        }


    }
}
