// CampusLifeAPI.Application/DTOs/Auth/LoginDto.cs
namespace CampusLifeAPI.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}