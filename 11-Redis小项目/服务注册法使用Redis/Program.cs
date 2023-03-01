//using Zack.ASPNETCore;
using 服务注册法使用Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(opt =>//连接Redis
{
    opt.Configuration = "localhost";
    opt.InstanceName = "ser_";
});
builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();//Redis缓存帮助类：缓存雪崩，混用时间策略
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
