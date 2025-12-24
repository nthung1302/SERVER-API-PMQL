using Files.Helpers;
using Files.Models.Request;
using Files.Services;
using Microsoft.AspNetCore.Mvc;

namespace Files.Controllers
{
    [ApiController]
    [Route("api/auth/login")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            FileLogger.Log($"Login attempt: {request.UserName}");

            var result = _service.Login(request);

            if (result.Code == null)
            {
                FileLogger.Log($"Login FAILED: {request.UserName}", "ERROR");
                return Unauthorized(result);
            }

            FileLogger.Log($"Login SUCCESS: {request.UserName}");
            return Ok(result);
        }
    }
}
