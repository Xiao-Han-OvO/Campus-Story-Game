// CampusLifeAPI.Application/Interfaces/IUserService.cs
using CampusLifeAPI.Application.DTOs.Auth;

namespace CampusLifeAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> UserExistsAsync(string username, string email);
        Task<string> GenerateJwtTokenAsync(int userId, string username);
    }
}