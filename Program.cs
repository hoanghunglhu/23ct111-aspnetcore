using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity; 
using Microsoft.EntityFrameworkCore.SqlServer;
using LearnApiNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server (Code của bạn)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm dịch vụ Memory Cache (Code của bạn)
builder.Services.AddMemoryCache();


// === THÊM 2 DÒNG ĐĂNG KÝ EMAIL SERVICE TẠI ĐÂY ===

// 1. Đọc SmtpSettings từ appsettings.json
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// 2. Đăng ký IEmailService (Lỗi của bạn ở bước trước là thiếu dòng này)
builder.Services.AddTransient<IEmailService, EmailService>();

// ===============================================


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