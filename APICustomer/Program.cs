using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APICustomer.Data;
using APIAddress.Services;
using Microsoft.Extensions.Options;
using MongoDB;
using DataAPI.Data;
using APICustomer.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APICustomerContext") ?? throw new InvalidOperationException("Connection string 'APICustomerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Arquivo de config
builder.Services.Configure<MongoDBAPIDataBaseSettings>(
               builder.Configuration.GetSection(nameof(MongoDBAPIDataBaseSettings)));


builder.Services.AddSingleton<IMongoDBAPIDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBAPIDataBaseSettings>>().Value);

builder.Services.AddSingleton<AddressesService>();
builder.Services.AddSingleton<CustomersService>();
#endregion

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
