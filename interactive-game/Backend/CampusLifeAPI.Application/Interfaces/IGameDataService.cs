// CampusLifeAPI.Application/Interfaces/IGameDataService.cs
using CampusLifeAPI.Application.DTOs.Game;

namespace CampusLifeAPI.Application.Interfaces
{
    public interface IGameDataService
    {
        Task<GameDataDto> GetGameDataAsync(int userId);
        Task UpdateGameDataAsync(int userId, GameDataDto gameDataDto);
        Task InitializeGameDataAsync(int userId);
    }
}