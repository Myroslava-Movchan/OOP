namespace Online_Store_Management.Models
{
    public struct ProductStruct
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }

        public ProductStruct(int productId, string? productName, decimal productPrice, int? productQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productQuantity;
        }
    }
}
