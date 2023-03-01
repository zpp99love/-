//using Zack.ASPNETCore;
using ����ע�ᷨʹ��Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(opt =>//����Redis
{
    opt.Configuration = "localhost";
    opt.InstanceName = "ser_";
});
builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();//Redis��������ࣺ����ѩ��������ʱ�����
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
