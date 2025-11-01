using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity; // Giả sử Entity ở đây
using LearnApiNetCore.Services; // Thêm namespace cho các Services

var builder = WebApplication.CreateBuilder(args);

// --- CẤU HÌNH DỊCH VỤ GỬI EMAIL (SMTP) ---
// 1. Đọc và ánh xạ cấu hình từ mục "SmtpSettings" trong appsettings.json
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));

// 2. Đăng ký IEmailService và EmailService vào Dependency Injection
// AddTransient là phù hợp cho các dịch vụ ngắn hạn như gửi email.
builder.Services.AddTransient<IEmailService, EmailService>();
// -------------------------------------------

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//Register services
builder.Services.AddHostedService<MyHostedService>(); // Giả sử MyHostedService tồn tại

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();