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
        public int Grade { get; set; } = 1;
        public int Semester { get; set; } = 1;
        public int Day { get; set; } = 1;
        public string? CurrentTaskId { get; set; } // 改为可为空
        
        public DateTime UpdatedAt { get; set; }
        
        // 导航属性
        public virtual User? User { get; set; }
    }
}
