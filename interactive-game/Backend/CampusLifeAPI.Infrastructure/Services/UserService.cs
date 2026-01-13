// CampusLifeAPI.Infrastructure/Services/UserService.cs
using CampusLifeAPI.Application.DTOs.Auth;
using CampusLifeAPI.Application.Interfaces;
using CampusLifeAPI.Domain.Entities;
using CampusLifeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CampusLifeAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IGameDataService _gameDataService;
        
        public UserService(ApplicationDbContext context, IConfiguration configuration, IGameDataService gameDataService)
        {
            _context = context;
            _configuration = configuration;
            _gameDataService = gameDataService;
        }
        
        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }
        
        public async Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // 检查用户是否已存在
            if (await UserExistsAsync(registerDto.Username, registerDto.Email))
            {
                throw new Exception("用户名或邮箱已存在");
            }
            
            // 使用简单的哈希方法（稍后我们会修复BCrypt问题）
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password), // 临时解决方案
                DisplayName = registerDto.DisplayName ?? registerDto.Username,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            // 初始化游戏数据
            await _gameDataService.InitializeGameDataAsync(user.Id);
            
            // 生成JWT Token
            var token = await GenerateJwtTokenAsync(user.Id, user.Username);
            
            // 获取游戏数据
            var gameData = await _gameDataService.GetGameDataAsync(user.Id);
            
            return new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName,
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(1440), // 24小时
                GameData = gameData
            };
        }
        
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.GameData)
                .FirstOrDefaultAsync(u => u.Username == loginDto.UsernameOrEmail || u.Email == loginDto.UsernameOrEmail);
            
            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("用户名或密码错误");
            }
            
            if (!user.IsActive)
            {
                throw new Exception("账户已被禁用");
            }
            
            // 更新最后登录时间
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            // 生成JWT Token
            var token = await GenerateJwtTokenAsync(user.Id, user.Username);
            
            // 获取游戏数据
            var gameData = await _gameDataService.GetGameDataAsync(user.Id);
            
            return new LoginResponseDto
            {
                UserId = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName,
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(1440),
                GameData = gameData
            };
        }
        
        // 临时密码哈希方法
        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password + "salt")); // 简单哈希，生产环境需要BCrypt
        }
        
        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
        
        public async Task<string> GenerateJwtTokenAsync(int userId, string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secret = "YourSuperSecretKeyHereThatIsAtLeast32CharactersLong123!"; // 临时硬编码
            var issuer = "CampusLifeAPI";
            var audience = "CampusLifeVueApp";
            var expireMinutes = 1440;
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}