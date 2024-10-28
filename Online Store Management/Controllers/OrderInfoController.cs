using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;
using Online_Store_Management.Services;


namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderInfoController : ControllerBase
    {
        private readonly IOrderInfo orderInfoService;

        public OrderInfoController(IOrderInfo orderInfoService)
        {
            this.orderInfoService = orderInfoService;
        }

        [HttpPost]
        public OrderInfo Post(Product product)
        {
            var orderInfo = orderInfoService.Post(product);
            var addToTable = orderInfoService.AddToTable(orderInfo);
            return orderInfo;
        }

        [HttpPost("CompareOrders")]
        public bool Compare(OrderInfo order)
        {
            var answer = orderInfoService.CompareOrders(order);
            return answer;
        }

    }
}
