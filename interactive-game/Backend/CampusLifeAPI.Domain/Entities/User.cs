// CampusLifeAPI.Domain/Entities/User.cs
using System;

namespace CampusLifeAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }
        
        // 简化：只保留必要的一对一关系
        public virtual GameData GameData { get; set; }
    }
}