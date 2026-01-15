using CampusLifeAPI.Application.DTOs.Auth;
using CampusLifeAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CampusLifeAPI.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        
        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                _logger.LogInformation("注册请求: {Username}", registerDto.Username);
                var result = await _userService.RegisterAsync(registerDto);
                
                // 设置Cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = result.Expires,
                    SameSite = SameSiteMode.Lax,
                    Secure = false
                };
                
                Response.Cookies.Append("auth_token", result.Token, cookieOptions);
                
                return Ok(new
                {
                    success = true,
                    message = "注册成功",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "注册失败: {Message}", ex.Message);
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                    innerException = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _userService.LoginAsync(loginDto);
                
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = loginDto.RememberMe ? result.Expires.AddDays(30) : result.Expires,
                    SameSite = SameSiteMode.Lax,
                    Secure = false
                };
                
                Response.Cookies.Append("auth_token", result.Token, cookieOptions);
                
                return Ok(new
                {
                    success = true,
                    message = "登录成功",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth_token");
            return Ok(new
            {
                success = true,
                message = "登出成功"
            });
        }
        
        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            var token = Request.Cookies["auth_token"];
            return Ok(new
            {
                isAuthenticated = !string.IsNullOrEmpty(token)
            });
        }
    }
}
