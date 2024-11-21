namespace Online_Store_Management.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? Category { get; set; }
        public bool Availability { get; set; }
        public int Rating { get; set; }

        public Product() { }

        public Product(int productId, string productName, int productPrice, string category, bool available, int rating)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Category = category;
            Availability = available;
            Rating = rating;
            
        }
    }
}
