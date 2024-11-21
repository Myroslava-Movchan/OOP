using System.ComponentModel.DataAnnotations;
namespace Online_Store_Management.Models
{
    public class OrderInfo
    {
        [Key]
        public int OrderNumber { get; set; }

        public string? Gift { get; set; }
        public required Product Product { get; set; }
        public string? Status { get; set; }

        public void ProductInfo(Product product)
        {
            this.Product = product;
        }

        public override bool Equals(object? obj)
        {
            return obj is OrderInfo info &&
                   OrderNumber == info.OrderNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber);
        }
    }


}
