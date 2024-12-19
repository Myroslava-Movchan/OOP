using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Management.Interfaces;
using Online_Store_Management.Requests;

namespace Online_Store_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, IJwtService jwtService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost("login")]
        public IActionResult Login([FromBody] Requests.LoginRequest request)
        {
            var user = _userService.GetUserByEmail(request.Email);

            if (user == null || !_userService.VerifyHashedPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _jwtService.GenerateToken(request.Email);
            return Ok(new { Token = token });
        }
    }
}
