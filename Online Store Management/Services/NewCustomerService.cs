using Online_Store_Management.Models;

namespace Online_Store_Management.Services
{
    public class NewCustomerService : INewCustomer
    {

        private static readonly string[] LastNamesNew = new[]
        {
            "Snow", "Goth", "White", "Jeffry", "Smith", "Brown"
        };

        private static readonly string[] Products = new[]
        {
            "T-Shirt", "Jeans", "Sweater", "Jacket", "Dress"
        };

        private static readonly int[] PostIndexes = new[] { 03115, 22567, 89088 };


        public async Task<NewCustomer> GetNewCustomerAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);

            var productName = Products[Random.Shared.Next(Products.Length)];
            var lastName = LastNamesNew[Random.Shared.Next(LastNamesNew.Length)];
            var customer = new NewCustomer
            {
                LastName = lastName,
                Id = Random.Shared.Next(1, 6),
                PostIndex = PostIndexes[Random.Shared.Next(PostIndexes.Length)]
            };
            var discountedPrice = customer.GetDiscount();

            return customer;
        }
    }
}
