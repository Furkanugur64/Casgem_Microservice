using AutoMapper;
using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Catalog.DTOs.CategoryDTOs;
using CasgemMicroservices.Catalog.Models;
using CasgemMicroservices.Catalog.Settings.Abstract;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace CasgemMicroservices.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client=new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CreateCategoryDTO>> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var value = _mapper.Map<Category>(createCategoryDTO);
            await _categoryCollection.InsertOneAsync(value);
            return Response<CreateCategoryDTO>.Success(_mapper.Map<CreateCategoryDTO>(value), 200);
        }

        public async Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            var value=await _categoryCollection.DeleteOneAsync(x=>x.CategoryID==id);
            if(value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek katagori Bulunamadı", 404);
            }
        }

        public async Task<Response<ResultCategoryDTO>> GetCategoryByIdAsync(string id)
        {
            var value=await _categoryCollection.Find<Category>(x=>x.CategoryID==id).FirstOrDefaultAsync();
            if (value==null)
            {
                return Response<ResultCategoryDTO>.Fail("Böyle Bir ID Bulunamadı", 404);
            }
            else
            {
                return Response<ResultCategoryDTO>.Success(_mapper.Map<ResultCategoryDTO>(value), 200);
            }
        }

        public async Task<Response<List<ResultCategoryDTO>>> GetCategoryListAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<ResultCategoryDTO>>.Success(_mapper.Map<List<ResultCategoryDTO>>(values),200);
        }

        public async Task<Response<UpdateCategoryDTO>> UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var value = _mapper.Map<Category>(updateCategoryDTO);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateCategoryDTO.CategoryID,value);
            if (result==null)
            {
                return Response<UpdateCategoryDTO>.Fail("Güncellenecek Veri Bulunamadı", 404);
            }
            else
            {
                return Response<UpdateCategoryDTO>.Success(204);
            }
        }
    }
}
