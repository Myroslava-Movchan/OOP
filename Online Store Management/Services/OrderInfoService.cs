using Online_Store_Management.Models;
using System.Collections;
using Online_Store_Management.Interfaces;
namespace Online_Store_Management.Services
{
    public class OrderInfoService : IOrderInfo
    {
        private readonly ArrayList orders = new ArrayList(100);
        private HashSet<OrderInfo> orderTable = new HashSet<OrderInfo>();
        private static readonly string[] Gifts =
        [
            "Pin", "Sticker",
            "Candy", "Bracelet",
            "Hairclip", "Socks"
        ];

        public async Task<OrderInfo> PostAsync(Product product, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var gifts = Gifts[Random.Shared.Next(Gifts.Length)];
            var orderInfo = new OrderInfo()
            {
                OrderNumber = Random.Shared.Next(1, 250),
                Gift = gifts,
                ProductName = product.ProductName,
                ProductId = product.ProductId,
                ProductPrice = product.ProductPrice,
                ProductQuantity = product.ProductQuantity,
                Delivery = await EstimateDeliveryAsync(product, cancellationToken)
            };

            object objOrder = orderInfo;
            orders.Add(objOrder);
            OrderInfo order = (OrderInfo)objOrder;
            return order;
        }

        public async Task<bool> CompareOrdersAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            bool compare = false;
            foreach (var existingOrder in orders)
            {
                if (order.Equals(existingOrder))
                {
                    compare = true;
                    break;
                }
            }
            return compare;
        }

        public async Task<bool> AddToTableAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            var orderHashCode = order.GetHashCode();
            foreach (var existingOrder in orders)
            {
                if (existingOrder.GetHashCode() == orderHashCode)
                {
                    return false;
                }
            }
            orderTable.Add(order);
            return true;
        }
        public async Task<int> EstimateDeliveryAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(50, cancellationToken);
            int delieverySum = 120;
            return delieverySum;
        }

        public async Task<int> EstimateDeliveryAsync(Product product, CancellationToken cancellationToken)
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
