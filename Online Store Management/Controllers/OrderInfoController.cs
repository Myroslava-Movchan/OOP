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
        public async Task<OrderInfo> PostAsync(Product product, CancellationToken cancellationToken)
        {
            var orderInfo = await orderInfoService.PostAsync(product, cancellationToken);
            var addToTable = orderInfoService.AddToTableAsync(orderInfo, cancellationToken);
            return orderInfo;
        }

        [HttpPost("CompareOrders")]
        public async Task<bool> CompareAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            var answer = await orderInfoService.CompareOrdersAsync(order, cancellationToken);
            return answer;
        }

    }
}
