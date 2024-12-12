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
    public class OrderInfoController : ControllerBase
    {
        private readonly IOrderInfo orderInfoService;

        public OrderInfoController(IOrderInfo orderInfoService)
        {
            this.orderInfoService = orderInfoService;
        }

        [HttpPost("Get information for order placement")]
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

            try
            {
                string decryptedStatus = orderInfoService.DecryptMessage(order.Status);
                order.Status = decryptedStatus;
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error decrypting status: " + ex.Message });
            }

            return Ok(new
            {
                order.Status,
                order.Product
            });
        }


        [HttpPut("Update order status")]
        public async Task UpdateAsync(OrderInfo order, CancellationToken cancellationToken)
        {
            await orderInfoService.UpdateAsync(order, cancellationToken);
        }

    }
}
