using HostedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ReadFileHostedService>();//正常测试
builder.Services.AddHostedService<ExceptionHostedService>();//托管发生异常怎么办
builder.Services.AddScoped<SumTest>();
builder.Services.AddHostedService<ScopeHostedService>();//托管服务是Singleton，所以不能注入小于Singleton的其他服务，只能通过IServiceScopeFactory
builder.Services.AddHostedService<ScheduledHostedService>();//定时执行（可以保证多少时间之后，但不能指定某个时间）
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
