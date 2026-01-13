// CampusLifeAPI.Application/DTOs/Auth/RegisterDto.cs
namespace CampusLifeAPI.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }
}