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
        private static readonly string[] Gifts = new[]
        {
            "Pin", "Sticker",
            "Candy", "Bracelet",
            "Hairclip", "Socks"
        };

        public OrderInfoController()
        {
            orderInfoService = new OrderInfoService();
        }

        [HttpPost]
        public OrderInfo Post(Product product)
        {
            var orderInfo = new OrderInfo();
            return orderInfo;
        }

        
    }
}
