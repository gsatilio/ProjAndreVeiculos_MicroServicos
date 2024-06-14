using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APIBank_SendMessageSQL.Data;
using Microsoft.Extensions.Options;
using APIBank_SQL.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<APIBank_SendMessageSQLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIBank_SendMessageSQLContext") ?? throw new InvalidOperationException("Connection string 'APIBank_SendMessageSQLContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Arquivo de config

builder.Services.AddSingleton<BankSendMessageSQLService>();

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
