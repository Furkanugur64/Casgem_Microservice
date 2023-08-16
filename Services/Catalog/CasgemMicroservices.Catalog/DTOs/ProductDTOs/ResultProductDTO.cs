
namespace CasgemMicroservices.Catalog.DTOs.ProductDTOs
{
    public class ResultProductDTO
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }        
        public decimal Price { get; set; }
        public int Stock { get; set; }       
        public string CategoryID { get; set; }
    }
}
