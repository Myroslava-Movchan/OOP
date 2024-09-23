namespace Online_Store_Management.Models
{
    public class OrderInfo : Product
    {
        public int OrderNumber { get; set; }

        public string? Gift { get; set; }

        public void ProductInfo(Product product)
        {
            this.ProductName = product.ProductName;
            this.ProductPrice = product.ProductPrice;
            this.ProductId = product.ProductId;
        }

    }


}
