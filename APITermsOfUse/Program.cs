using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using APITermsOfUse.Data;
using DataAPI.Data;
using Microsoft.Extensions.Options;
using MongoDB;
using APITermsOfUse.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APITermsOfUseContext") ?? throw new InvalidOperationException("Connection string 'APITermsOfUseContext' not found.")));

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

builder.Services.AddSingleton<TermsOfUseAgreementService>();
builder.Services.AddSingleton<TermsOfUseService>();

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
