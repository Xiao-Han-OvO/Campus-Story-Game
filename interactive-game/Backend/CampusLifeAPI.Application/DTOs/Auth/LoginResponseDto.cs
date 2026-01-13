namespace CampusLifeAPI.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public GameDataDto GameData { get; set; }
    }
}