using Online_Store_Management.Models;
namespace Online_Store_Management.Services
{
    public class OrderInfoService
    {
        private readonly List<OrderInfo> orders = new List<OrderInfo>();
        private static readonly string[] Gifts =
        [
            "Pin", "Sticker",
            "Candy", "Bracelet",
            "Hairclip", "Socks"
        ];

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
            orders.Add(orderInfo);
            return orderInfo;
        }

        public bool CompareOrders(OrderInfo order)
        {
            bool compare = false;
            foreach(var existingOrder in orders)
            {
                if (order.Equals(existingOrder))
                {
                    compare = true;
                    break;
                }
            }
            return compare;
        }
    }
}
