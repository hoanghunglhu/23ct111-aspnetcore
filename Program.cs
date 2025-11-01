using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity; // Giả sử AppDbContext và NewsArticle ở đây
using Microsoft.EntityFrameworkCore.SqlServer;
using LearnApiNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server (Code của bạn)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// === THÊM DÒNG NÀY ===
// Thêm dịch vụ Memory Cache để thực hiện yêu cầu cache
builder.Services.AddMemoryCache();
// ======================

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//Register services
builder.Services.AddHostedService<MyHostedService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();