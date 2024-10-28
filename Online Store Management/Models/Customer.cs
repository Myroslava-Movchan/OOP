namespace Online_Store_Management.Models
{

    public abstract class Customer
    {
        private Product _product;

        public Product GetProduct()
        { return _product; }

        public void SetProduct(Product value)
        { _product = value; }
        public string? LastName { get; set; }
        public int PostIndex { get; set; }
        public int Id { get; set; }
        public abstract decimal GetDiscount();
        public virtual void Help(string issue)
        {
            Console.WriteLine($"The assistant will answer during one week to help you with your issue: {issue}.");
        }
        public void Recommendation()
        {
            Console.WriteLine("Turn on your notifications to receive information about new products!");
        }
    }
    public class Discount
    {
        public Customer? Customer { get; set; }
        public decimal DiscountedPrice { get; set; }
        public const decimal minPrice = 10m;
        public bool IsPriceOk(decimal discounted)
        {
            if (discounted >= minPrice)
            {
                return true;
            }
            return false;
        }
        public static decimal GetMinPrice()
        {
            return minPrice;
        }
    }
}
