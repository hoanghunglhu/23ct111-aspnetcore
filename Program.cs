using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity;
using LearnApiNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Register hosted services
builder.Services.AddHostedService<MyHostedService>();
builder.Services.AddHostedService<EmailHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
