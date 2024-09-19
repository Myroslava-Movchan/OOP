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
       
        public int Id { get; set; }

        public abstract decimal GetDiscount();
        public virtual void Help(string issue)
        {
            Console.WriteLine($"The assistant will answer during one week to help you with you issue: {issue}.");
        }

    }

    public class NewCustomer : Customer
    {
        public override decimal GetDiscount()
        {
            Product product = GetProduct();

            decimal fullPrice = product.ProductPrice;
            decimal discounted = fullPrice - (fullPrice * 0.15m);
            return discounted;
        }
    }

    public class RegularCustomer : Customer
    {
        public override void Help(string issue)
        {
            base.Help(issue);
            Console.WriteLine($"Your assistant will answer during 2 days to help with your issue: {issue}");
        }
        public override decimal GetDiscount()
        {   
            Product product = GetProduct();

            decimal fullPrice = product.ProductPrice;
            decimal discounted = fullPrice - (fullPrice * 0.10m);
            return discounted;
        }
    }
}
