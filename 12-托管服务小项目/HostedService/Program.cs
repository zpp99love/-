using HostedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<ReadFileHostedService>();//��������
builder.Services.AddHostedService<ExceptionHostedService>();//�йܷ����쳣��ô��
builder.Services.AddScoped<SumTest>();
builder.Services.AddHostedService<ScopeHostedService>();//�йܷ�����Singleton�����Բ���ע��С��Singleton����������ֻ��ͨ��IServiceScopeFactory
builder.Services.AddHostedService<ScheduledHostedService>();//��ʱִ�У����Ա�֤����ʱ��֮�󣬵�����ָ��ĳ��ʱ�䣩
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
