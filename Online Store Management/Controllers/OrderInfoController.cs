using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Models;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderInfoController : ControllerBase
    {
        private readonly OrderInfoService orderInfoService;

        public OrderInfoController()
        {
            orderInfoService = new OrderInfoService();
        }

        [HttpPost]
        public OrderInfo Post(Product product)
        {
            var orderInfo = orderInfoService.Post;
            var addToTable = orderInfoService.AddToTable;
            return orderInfo(product);
        }

        [HttpPost("CompareOrders")]
        public bool Compare(OrderInfo order)
        {
            var answer = orderInfoService.CompareOrders(order);
            return answer;
        }

    }
}
