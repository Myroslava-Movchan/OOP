namespace Online_Store_Management.Models
{
    public class RegularCustomer : Customer
    {
        public delegate decimal RegularCustomerDiscount();

        public static decimal regularDiscount = 0.10m;

        public decimal ExecuteDiscount(RegularCustomerDiscount discount)
        {
            return discount();
        }

        public override decimal GetDiscount()
        {
            return regularDiscount;
        }
        public override void Help(string issue)
        {
            base.Help(issue);
            Console.WriteLine($"Your assistant will answer during 2 days to help with your issue: {issue}");
        }
        public new void Recommendation()
        {
            Console.WriteLine("Do not forget to check your email for new offers!");
        }
    }
}
