using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Discount.DTOs;
using CasgemMicroservices.Discount.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace CasgemMicroservices.Discount.Services
{
    public class CouponService : ICouponService
    {
       private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public CouponService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> CreateCoupon(CreateCouponDTO createCouponDTO)
        {
            var values = await _dbConnection.ExecuteAsync("insert into Coupon (Rate,Code,CreatedTime) values (@Rate,@Code,@Createdtime)",createCouponDTO);
            if (values > 0)
            {
                return Response<NoContent>.Success(200);
            }           
            return Response<NoContent>.Fail("Kupon Ekleme İşleminde Hata Oluştu !",500);          
        }

        public async Task<Response<NoContent>> DeleteCoupon(int id)
        {
            var values = await _dbConnection.ExecuteAsync("delete from Coupon where CouponID=@CouponId", new { CouponID = id });
            return values > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Kupon Bulunamadı", 404);
        }

        public async Task<Response<ResultCouponDTO>> GetCouponByID(int id)
        {
            var values = (await _dbConnection.QueryAsync<ResultCouponDTO>("select * from Coupon where CouponID=@CouponID")).FirstOrDefault();
            var parameters = new DynamicParameters();
            parameters.Add("@CouponID", id);
            if(values == null)
            {
                return Response<ResultCouponDTO>.Fail("Kupon Bulunamadı", 404);
            }          
            return Response<ResultCouponDTO>.Success(values, 200);
        }

        public async Task<Response<List<ResultCouponDTO>>> GetCouponList()
        {
            var values = await _dbConnection.QueryAsync<ResultCouponDTO>("select * from Coupon");
            return Response<List<ResultCouponDTO>>.Success(values.ToList(), 200);
        }

        public async Task<Response<NoContent>> UpdateCoupon(UpdateCouponDTO updateCouponDTO)
        {
            var values = await _dbConnection.ExecuteAsync("Update Coupon set Code=@Code , Rate=@Rate where CouponID=@CouponID");
            var parameters = new DynamicParameters();
            parameters.Add("@Rate", updateCouponDTO.Rate);
            parameters.Add("@Code", updateCouponDTO.Code);
            parameters.Add("@CouponID", updateCouponDTO.CouponID);
            if (values > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Kupon Bulunamadı", 404);
        }
    }
}
