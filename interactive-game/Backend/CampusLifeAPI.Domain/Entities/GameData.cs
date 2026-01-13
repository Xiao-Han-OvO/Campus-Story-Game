// CampusLifeAPI.Domain/Entities/GameData.cs
namespace CampusLifeAPI.Domain.Entities
{
    public class GameData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        // 基础状态
        public int Health { get; set; } = 100;
        public int Energy { get; set; } = 100;
        public int Mood { get; set; } = 80;
        public int HealthStatus { get; set; } = 90;
        public int Money { get; set; } = 1000;
        public int Experience { get; set; } = 0;
        public int Reputation { get; set; } = 50;
        
        // 属性状态
        public int Intelligence { get; set; } = 10;
        public int Strength { get; set; } = 10;
        public int Social { get; set; } = 10;
        public int Efficiency { get; set; } = 10;
        public int Creativity { get; set; } = 10;
        public int Learning { get; set; } = 10;
        
        // 游戏进度
        public int Grade { get; set; } = 1; // 年级 1-3
        public int Semester { get; set; } = 1; // 学期 1-2
        public int Day { get; set; } = 1; // 游戏内天数
        public string CurrentTaskId { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        // 导航属性
        public virtual User User { get; set; }
    }
}