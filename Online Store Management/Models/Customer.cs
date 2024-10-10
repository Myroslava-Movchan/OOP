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
            Console.WriteLine($"The assistant will answer during one week to help you with you issue: {issue}.");
        }

    }

    public class NewCustomer : Customer
    {
        public static decimal newDiscount = 0.15m;

        public override decimal GetDiscount()
        {

            Product product = GetProduct();

            decimal fullPrice = product.ProductPrice;
            decimal discounted = fullPrice - (fullPrice * newDiscount);
            return discounted;
        }
    }

    public class RegularCustomer : Customer
    {
        public static decimal regularDiscount = 0.10m;
        public override void Help(string issue)
        {
            base.Help(issue);
            Console.WriteLine($"Your assistant will answer during 2 days to help with your issue: {issue}");
        }
        public override decimal GetDiscount()
        {
            Product product = GetProduct();

            decimal fullPrice = product.ProductPrice;
            decimal discounted = fullPrice - (fullPrice * regularDiscount);
            return discounted;
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
