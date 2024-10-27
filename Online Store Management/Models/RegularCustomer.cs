namespace Online_Store_Management.Models
{
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
        public new void Recommendation()
        {
            Console.WriteLine("Do not forget to check your email for new offers!");
        }
    }
}
