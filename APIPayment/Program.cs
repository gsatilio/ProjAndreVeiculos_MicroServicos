using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIPayment.Data;
using DataAPI.Data;
using APIPayment.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIPaymentContext") ?? throw new InvalidOperationException("Connection string 'APIPaymentContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PaymentsService>();
builder.Services.AddSingleton<PixesService>();
builder.Services.AddSingleton<PixTypesService>();
builder.Services.AddSingleton<BoletosService>();
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
