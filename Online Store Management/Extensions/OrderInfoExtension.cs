using Online_Store_Management.Models;

namespace Online_Store_Management.Extensions
{
    public static class OrderInfoExtension
    {
        public static async Task<int> EstimateDeliveryAsync(this Product product, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            int delieverySum = 100;
            if (product.ProductQuantity >= 6)
            {
                delieverySum = 80;
            }
            return delieverySum;
        }
    }
}
