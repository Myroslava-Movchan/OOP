namespace Online_Store_Management.Models
{
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
        public new void Recommendation()
        {
            Console.WriteLine("Order 2+ products and get a free sample!");
        }
    }
}
