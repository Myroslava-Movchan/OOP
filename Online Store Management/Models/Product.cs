namespace Online_Store_Management.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }
    }
}
