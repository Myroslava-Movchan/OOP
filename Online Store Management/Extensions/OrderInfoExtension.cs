using Online_Store_Management.Models;

namespace Online_Store_Management.Extensions
{
    public static class OrderInfoExtension
    {
        public static  int EstimateDeliveryAsync(this Product product, CancellationToken cancellationToken)
        {
            int delieverySum = 100;
            if (product.ProductPrice >= 200)
            {
                delieverySum = 80;
            }
            return  delieverySum;
        }
    }
}
