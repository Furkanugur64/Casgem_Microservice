namespace CasgemMicroservices.Discount.DTOs
{
    public class UpdateCouponDTO
    {
        public int CouponID { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
