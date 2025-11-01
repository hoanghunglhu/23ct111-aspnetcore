using LearnApiNetCore.Models;
using LearnApiNetCore.Services;
using LearnApiNetCore.Workers; // <-- Thêm namespace của Worker

var builder = WebApplication.CreateBuilder(args);

// *** CẤU HÌNH DỊCH VỤ ***

// 1. Đăng ký cấu hình SmtpSettings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("MailSettings"));

// 2. Đăng ký EmailService (dạng Transient)
builder.Services.AddTransient<IEmailService, EmailService>();

// 3. Đăng ký Background Service (dạng HostedService)
builder.Services.AddHostedService<AutoEmailWorker>();

// *************************

// Các dịch vụ có sẵn
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();