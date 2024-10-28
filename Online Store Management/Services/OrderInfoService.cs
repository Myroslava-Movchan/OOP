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

        public OrderInfo Post(Product product)
        {
            var gifts = Gifts[Random.Shared.Next(Gifts.Length)];
            var orderInfo = new OrderInfo()
            {
                OrderNumber = Random.Shared.Next(1, 250),
                Gift = gifts,
                ProductName = product.ProductName,
                ProductId = product.ProductId,
                ProductPrice = product.ProductPrice,
                ProductQuantity = product.ProductQuantity,
                Delivery = EstimateDelivery(product)
            };

            object objOrder = orderInfo;
            orders.Add(objOrder);
            OrderInfo order = (OrderInfo)objOrder;
            return order;
        }

        public bool CompareOrders(OrderInfo order)
        {
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

        public bool AddToTable(OrderInfo order)
        {
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
        public int EstimateDelivery()
        {
            int delieverySum = 120;
            return delieverySum;
        }

        public int EstimateDelivery(Product product)
        {
            int delieverySum = 100;
            if (product.ProductQuantity >= 6)
            {
                delieverySum = 80;
            }
            return delieverySum;
        }

    }
}
