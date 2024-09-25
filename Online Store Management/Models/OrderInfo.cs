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

        public override bool Equals(object? obj)
        {
            if (obj is OrderInfo other)
            {
                return this.OrderNumber == other.OrderNumber;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber);
        }
    }


}
