using DatosPesca.Context;
using DatosPesca.Servicio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatosPescaContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PescaDB;Trusted_Connection=True;"));
builder.Services.AddScoped<ServicioBD>();
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
