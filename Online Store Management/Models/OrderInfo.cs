using System.ComponentModel.DataAnnotations;
namespace Online_Store_Management.Models
{
    public class OrderInfo
    {
        [Key]
        public int OrderNumber { get; set; }

        public string? Gift { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }

        public void ProductInfo(Product product)
        {
            this.Product = product;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not OrderInfo other) return false;
            return OrderNumber == other.OrderNumber;
        }

        public override int GetHashCode()
        {
            return OrderNumber.GetHashCode();
        }
    }


}
