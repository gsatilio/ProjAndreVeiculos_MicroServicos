using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIFinancing.Data;
using DataAPI.Data;
using APIFinancing.Services;
using APISale.Services;
using APIBank.Services;
using Microsoft.Extensions.Options;
using MongoDB;
using DataAPI.Service;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIFinancingContext") ?? throw new InvalidOperationException("Connection string 'APIFinancingContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBAPIDataBaseSettings>(
               builder.Configuration.GetSection(nameof(MongoDBAPIDataBaseSettings)));

builder.Services.AddSingleton<IMongoDBAPIDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBAPIDataBaseSettings>>().Value);

builder.Services.AddSingleton<FinancingsService>();
builder.Services.AddSingleton<DataAPIServices>();
//builder.Services.AddSingleton<BanksService>();
//builder.Services.AddSingleton<SalesService>();
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
