using Online_Store_Management.Models;

namespace Online_Store_Management.Services
{
    public class ProductPriceService
    {
        private static Product product = new Product();
        private readonly decimal price = product.ProductPrice;
        public ProductPriceType CategorizeGiftsNumber(OrderInfo orderInfo, Product product) => (orderInfo, product.ProductPrice) switch
        {
            (OrderInfo _, > 0 and <= 20) => ProductPriceType.Low,
            (OrderInfo _, >= 21 and <= 100) => ProductPriceType.Medium,
            (OrderInfo _, >= 101) => ProductPriceType.High,
            (_, <= 0) => ProductPriceType.InvalidPrice,
        };
    }
}
