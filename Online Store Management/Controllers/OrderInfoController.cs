using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Extensions;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Models;

namespace Online_Store_Management.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderInfoController(IOrderInfo orderInfoService) : ControllerBase
    {
        private readonly IOrderInfo orderInfoService = orderInfoService;

        [HttpPost("Get current time in Tokyo")]
        public DateTime GetTime()
        {
            return orderInfoService.GetTimeTokyo();
        }

        [HttpPost("Get information for order placement")]
        public async Task<OrderInfo> PostAsync(Product product, CancellationToken cancellationToken, DateTime time)
        {
            var orderInfo = await orderInfoService.PostAsync(product, time, cancellationToken);
            var addToTable = orderInfoService.AddToTableAsync(orderInfo, cancellationToken);
            return orderInfo;
        }

        [HttpPost("CompareOrders")]
        public async Task<bool> CompareAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            var answer = await orderInfoService.CompareOrdersAsync(order, cancellationToken);
            return answer;
        }

        [HttpPost("Get total sum")]
        public decimal CalculateTotal(OrderInfo order)
        {
            return orderInfoService.GetTotal(order);
        }

        [HttpPost("Place new order")]
        public async Task AddOrderAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await orderInfoService.AddOrderAsync(order, cancellationToken);
        }

        [HttpDelete("Delete order")]
        public async Task DeleteAsync(int orderNumber, CancellationToken cancellationToken)
        {
            await orderInfoService.DeleteAsync(orderNumber, cancellationToken);
        }

        [HttpGet("Get current information")]
        public async Task<ActionResult<OrderInfo>> GetByOrderNumberAsync(int orderNumber, CancellationToken cancellationToken)
        {
            var order = await orderInfoService.GetOrderByIdAsync(orderNumber, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            var frenchTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var frenchTime = TimeZoneInfo.ConvertTime(order.OrderDate, TimeZoneInfo.Utc, frenchTimeZone);

            return Ok(new
            {
                order.Status,
                order.Product,
                DateTime = order.OrderDate = frenchTime
            });
        }


        [HttpPut("Update order status")]
        public async Task UpdateAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await orderInfoService.UpdateAsync(order, cancellationToken);
        }

    }
}
