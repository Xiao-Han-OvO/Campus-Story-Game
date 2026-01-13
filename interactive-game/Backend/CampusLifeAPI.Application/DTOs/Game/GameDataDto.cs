// CampusLifeAPI.Application/DTOs/Game/GameDataDto.cs
namespace CampusLifeAPI.Application.DTOs.Game
{
    public class GameDataDto
    {
        public int Health { get; set; }
        public int Energy { get; set; }
        public int Mood { get; set; }
        public int HealthStatus { get; set; }
        public int Money { get; set; }
        public int Experience { get; set; }
        public int Reputation { get; set; }
        
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Social { get; set; }
        public int Efficiency { get; set; }
        public int Creativity { get; set; }
        public int Learning { get; set; }
        
        public int Grade { get; set; }
        public int Semester { get; set; }
        public int Day { get; set; }
        public string CurrentTaskId { get; set; }
    }
}