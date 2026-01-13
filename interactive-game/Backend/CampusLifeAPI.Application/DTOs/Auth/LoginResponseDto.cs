// CampusLifeAPI.Application/DTOs/Auth/LoginResponseDto.cs
using CampusLifeAPI.Application.DTOs.Game;

namespace CampusLifeAPI.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public GameDataDto GameData { get; set; } = new GameDataDto();
    }
}