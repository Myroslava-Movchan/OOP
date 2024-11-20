namespace Online_Store_Management.Models
{
    public class NewCustomer : Customer
    {
        public static decimal newDiscount = 0.15m;

        public override decimal GetDiscount()
        {
            return newDiscount;
        }
        public new void Recommendation()
        {
            Console.WriteLine("Order 2+ products and get a free sample!");
        }
    }
}
