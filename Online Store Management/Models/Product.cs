using Online_Store_Management.Infrastructure;
namespace Online_Store_Management.Models
{
    public class Product
    {
        public Logger Logger { get; set;}
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        public void CreateProduct()
        {
            Logger.Log($"Created product {ProductName}");
        }
        
    }
}
