// CampusLifeAPI.Domain/Entities/UserAchievement.cs
namespace CampusLifeAPI.Domain.Entities
{
    public class UserAchievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
    }
}