using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(opt => {
    opt.UseSqlServer("Server=.;Database=EFCore1;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
});

/*builder.Services.AddDbContext<MyDbContext2>(opt => {
    opt.UseSqlServer("Server=.;Database=EFCore2;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
});*/



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
