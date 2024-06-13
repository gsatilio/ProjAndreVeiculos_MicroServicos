using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIDependent.Data;
using DataAPI.Data;
using APIDependent.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIDependentContext") ?? throw new InvalidOperationException("Connection string 'APIDependentContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DependentsService>();
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
