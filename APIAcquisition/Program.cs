using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIAcquisition.Data;
using APIAcquisition.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APIAcquisitionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIAcquisitionContext") ?? throw new InvalidOperationException("Connection string 'APIAcquisitionContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AcquisitionService>(); // injeta dependencia
builder.Services.AddSingleton<Repositories.AcquisitionRepository>(); // injeta dependencia

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
