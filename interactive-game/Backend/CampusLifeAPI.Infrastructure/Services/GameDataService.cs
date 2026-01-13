// CampusLifeAPI.Infrastructure/Services/GameDataService.cs
using CampusLifeAPI.Application.DTOs.Game;
using CampusLifeAPI.Application.Interfaces;
using CampusLifeAPI.Domain.Entities;
using CampusLifeAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CampusLifeAPI.Infrastructure.Services
{
    public class GameDataService : IGameDataService
    {
        private readonly ApplicationDbContext _context;
        
        public GameDataService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<GameDataDto> GetGameDataAsync(int userId)
        {
            var gameData = await _context.GameData
                .FirstOrDefaultAsync(g => g.UserId == userId);
                
            if (gameData == null)
            {
                throw new Exception("游戏数据未找到");
            }
            
            return new GameDataDto
            {
                Health = gameData.Health,
                Energy = gameData.Energy,
                Mood = gameData.Mood,
                HealthStatus = gameData.HealthStatus,
                Money = gameData.Money,
                Experience = gameData.Experience,
                Reputation = gameData.Reputation,
                
                Intelligence = gameData.Intelligence,
                Strength = gameData.Strength,
                Social = gameData.Social,
                Efficiency = gameData.Efficiency,
                Creativity = gameData.Creativity,
                Learning = gameData.Learning,
                
                Grade = gameData.Grade,
                Semester = gameData.Semester,
                Day = gameData.Day,
                CurrentTaskId = gameData.CurrentTaskId
            };
        }
        
        public async Task UpdateGameDataAsync(int userId, GameDataDto gameDataDto)
        {
            var gameData = await _context.GameData
                .FirstOrDefaultAsync(g => g.UserId == userId);
                
            if (gameData == null)
            {
                throw new Exception("游戏数据未找到");
            }
            
            // 更新游戏数据
            gameData.Health = gameDataDto.Health;
            gameData.Energy = gameDataDto.Energy;
            gameData.Mood = gameDataDto.Mood;
            gameData.HealthStatus = gameDataDto.HealthStatus;
            gameData.Money = gameDataDto.Money;
            gameData.Experience = gameDataDto.Experience;
            gameData.Reputation = gameDataDto.Reputation;
            
            gameData.Intelligence = gameDataDto.Intelligence;
            gameData.Strength = gameDataDto.Strength;
            gameData.Social = gameDataDto.Social;
            gameData.Efficiency = gameDataDto.Efficiency;
            gameData.Creativity = gameDataDto.Creativity;
            gameData.Learning = gameDataDto.Learning;
            
            gameData.Grade = gameDataDto.Grade;
            gameData.Semester = gameDataDto.Semester;
            gameData.Day = gameDataDto.Day;
            gameData.CurrentTaskId = gameDataDto.CurrentTaskId;
            gameData.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
        }
        
        public async Task InitializeGameDataAsync(int userId)
        {
            var gameData = new GameData
            {
                UserId = userId,
                Health = 100,
                Energy = 100,
                Mood = 80,
                HealthStatus = 90,
                Money = 1000,
                Experience = 0,
                Reputation = 50,
                
                Intelligence = 10,
                Strength = 10,
                Social = 10,
                Efficiency = 10,
                Creativity = 10,
                Learning = 10,
                
                Grade = 1,
                Semester = 1,
                Day = 1,
                UpdatedAt = DateTime.UtcNow
            };
            
            _context.GameData.Add(gameData);
            await _context.SaveChangesAsync();
        }
    }
}