using Online_Store_Management.Models;
using static Online_Store_Management.Models.ProductTypeEnum;

namespace Online_Store_Management.Services
{
    public class ProductPriceService
    {
        public ProductPriceType CategorizeGiftsNumber(OrderInfo orderInfo, Product product) => (orderInfo, product.ProductPrice) switch
        {
            (OrderInfo _, > 0 and <= 20) => ProductPriceType.Low,
            (OrderInfo _, >= 21 and <= 100) => ProductPriceType.Medium,
            (OrderInfo _, >= 101) => ProductPriceType.High,
            (_, <= 0) => ProductPriceType.InvalidPrice,
            _ => ProductPriceType.Uncategorized,
        };
    }
}
