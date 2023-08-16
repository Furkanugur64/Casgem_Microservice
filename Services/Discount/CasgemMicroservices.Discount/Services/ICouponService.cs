using Casgem_Microservices.Shared.DTOs;
using CasgemMicroservices.Discount.DTOs;
using CasgemMicroservices.Discount.Models;

namespace CasgemMicroservices.Discount.Services
{
    public interface ICouponService
    {
        Task<Response<List<ResultCouponDTO>>> GetCouponList();
        Task<Response<NoContent>> CreateCoupon(CreateCouponDTO createCouponDTO);
        Task<Response<NoContent>> UpdateCoupon(UpdateCouponDTO createCouponDTO);
        Task<Response<NoContent>> DeleteCoupon(int id);
        Task<Response<ResultCouponDTO>> GetCouponByID(int id);
    }
}
