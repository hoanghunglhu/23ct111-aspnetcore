using Microsoft.Extensions.Caching.Memory;
var builder = WebApplication.CreateBuilder(args);
// chay lenh dotnet add package Swashbuckle.AspNetCore trong file 


// Thêm MemoryCache và Controllers
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Thêm dịch vụ Swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Chỉ bật Swagger khi chạy môi trường Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run(); 