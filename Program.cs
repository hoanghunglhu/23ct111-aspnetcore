using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ✅ Thêm các service trước khi Build()
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// ✅ Bật Swagger (phải thêm trước builder.Build())
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Tạo app (Build sau khi cấu hình xong services)
var app = builder.Build();

// ✅ Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
