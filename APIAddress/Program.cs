using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIAddress.Data;
using Microsoft.Extensions.Options;
using Services;
using MongoDatabase;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APIAddressContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIAddressContext") ?? throw new InvalidOperationException("Connection string 'APIAddressContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Arquivo de config
var config = builder.Configuration.GetSection(nameof(MongoDBAPIDataBaseSettings));
builder.Services.Configure<IMongoDBAPIDataBaseSettings>(config);


builder.Services.AddSingleton<IMongoDBAPIDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBAPIDataBaseSettings>>().Value);

builder.Services.AddSingleton<AddressService>();
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
