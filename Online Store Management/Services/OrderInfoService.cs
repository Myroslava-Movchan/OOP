using Online_Store_Management.Models;
namespace Online_Store_Management.Services
{
    public class OrderInfoService
    {
        private static readonly string[] Gifts = new[]
        {
            "Pin", "Sticker",
            "Candy", "Bracelet",
            "Hairclip", "Socks"
        };

        public OrderInfo Post(Product product)
        {
            var gifts = Gifts[Random.Shared.Next(Gifts.Length)];
            var orderInfo = new OrderInfo()
            {
                OrderNumber = Random.Shared.Next(1, 250),
                Gift = gifts,
                ProductName = product.ProductName,
                ProductId = product.ProductId,
                ProductPrice = product.ProductPrice
            };

            return orderInfo;
        }
    }
}
