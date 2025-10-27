using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Kích hoạt Controllers (cần thiết cho NewsController)
builder.Services.AddControllers();

// 2. Đăng ký IMemoryCache (Yêu cầu của Task 3)
builder.Services.AddMemoryCache();

// 3. Đăng ký Cache Service tùy chỉnh
builder.Services.AddSingleton<ICacheService, MemoryCacheService>(); 

// Cấu hình Swagger/OpenAPI (để bạn dễ dàng test)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. Map Controller endpoints (cho /news và /news/clear-cache)
app.MapControllers(); 

// *** Đã xóa toàn bộ code liên quan đến /weatherforecast ***

app.Run();