using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Services;

namespace Online_Store_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerializerController : ControllerBase
    {
        private readonly SerializerService _serializerService;

        public SerializerController(SerializerService serializerService)
        {
            _serializerService = serializerService;
        }

        [HttpGet("serialize")]
        public IActionResult Serialize()
        {
            _serializerService.Serialization();
            return Ok("Serialization completed and logged to file.");
        }
    }
}
