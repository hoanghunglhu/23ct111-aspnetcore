using LearnApiNetCore.Models;
using LearnApiNetCore.Services;
using LearnApiNetCore.Workers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddHostedService<AutoEmailWorker>();


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