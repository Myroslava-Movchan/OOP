using Online_Store_Management.Models;

namespace Online_Store_Management.Services
{
    public class RegularCustomerService : IRegularCustomer
    {

        private static readonly string[] LastNamesRegular = new[]
        {
            "Garcia", "Lee", "Patel", "Johnson", "Wilson", "Kim"
        };

        private static readonly string[] Products = new[]
        {
            "Hoodie", "Pajamas", "Belt", "Scarf", "Hat"
        };

        private static readonly int[] PostIndexes = new[] { 54321, 65678, 12345 };

        public async Task<RegularCustomer> GetRegularCustomerAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);

            var productName = Products[Random.Shared.Next(Products.Length)];
            var lastName = LastNamesRegular[Random.Shared.Next(LastNamesRegular.Length)];
            var postIndex = PostIndexes[Random.Shared.Next(PostIndexes.Length)];
            var customer = new RegularCustomer
            {
                LastName = lastName,
                Id = Random.Shared.Next(7, 12),
                PostIndex = postIndex
            };
            var product = new Product
            {
                ProductId = Random.Shared.Next(1, 18),
                ProductName = productName,
                ProductPrice = Random.Shared.Next(8, 230)
            };
            customer.SetProduct(product);
            RegularCustomer.RegularCustomerDiscount getDiscount = customer.GetDiscount;

            return customer;
        }
    }
}
