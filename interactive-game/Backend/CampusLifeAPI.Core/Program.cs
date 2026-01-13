// CampusLifeAPI.Core/Program.cs
using CampusLifeAPI.Core.Models;
using CampusLifeAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 添加服务到容器
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置数据库上下文
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameDataService, GameDataService>();

// 添加跨域配置（更新之前配置的CORS）
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueAppPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

// 配置JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.Zero
    };
});

// 配置CORS - 允许前端Vue应用访问
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueAppPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:3000") // Vue开发服务器地址
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // 允许Cookie
        });
});

var app = builder.Build();

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("VueAppPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();