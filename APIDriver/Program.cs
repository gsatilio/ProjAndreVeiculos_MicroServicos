using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIDriver.Data;
using DataAPI.Data;
using APIDriver.Services;
using APIAddress.Services;
using Microsoft.Extensions.Options;
using MongoDB;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIDriverContext") ?? throw new InvalidOperationException("Connection string 'APIDriverContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBAPIDataBaseSettings>(
               builder.Configuration.GetSection(nameof(MongoDBAPIDataBaseSettings)));


builder.Services.AddSingleton<IMongoDBAPIDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBAPIDataBaseSettings>>().Value);


builder.Services.AddSingleton<CategoriesService>();
builder.Services.AddSingleton<ConductorsService>();
builder.Services.AddSingleton<DriverLicensesService>();
builder.Services.AddSingleton<AddressesService>();


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
