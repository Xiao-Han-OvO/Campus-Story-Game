// CampusLifeAPI.Infrastructure/Data/ApplicationDbContext.cs
using CampusLifeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusLifeAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<GameData> GameData { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GameTask> Tasks { get; set; }
        public DbSet<TaskProgress> TaskProgresses { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<GameLog> GameLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // 配置User实体
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.DisplayName).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                
                // 一对一关系
                entity.HasOne(e => e.Profile)
                      .WithOne(p => p.User)
                      .HasForeignKey<UserProfile>(p => p.UserId);
                
                entity.HasOne(e => e.GameData)
                      .WithOne(g => g.User)
                      .HasForeignKey<GameData>(g => g.UserId);
            });
            
            // 配置GameData实体
            modelBuilder.Entity<GameData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Health).HasDefaultValue(100);
                entity.Property(e => e.Energy).HasDefaultValue(100);
                entity.Property(e => e.Mood).HasDefaultValue(80);
                entity.Property(e => e.HealthStatus).HasDefaultValue(90);
                entity.Property(e => e.Money).HasDefaultValue(1000);
                entity.Property(e => e.Grade).HasDefaultValue(1);
                entity.Property(e => e.Semester).HasDefaultValue(1);
                entity.Property(e => e.Day).HasDefaultValue(1);
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}