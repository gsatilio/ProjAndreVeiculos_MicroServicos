using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APICreditCard.Data;
using DataAPI.Data;
using APICarOperation.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APICreditCardContext") ?? throw new InvalidOperationException("Connection string 'APICreditCardContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CreditCardsService>();
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
