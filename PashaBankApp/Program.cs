using Microsoft.EntityFrameworkCore;
using PashaBankApp.DbContexti;
using PashaBankApp.Services;
using PashaBankApp.Services.Interface;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDistributor, DistributorServices>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IDistributorSale,DistributorSaleServices>();
builder.Services.AddScoped<IError, ErrorServices>();
builder.Services.AddScoped<ILog,LogServices>();
builder.Services.AddScoped<Ibonus, BonusServices>();
builder.Services.AddDbContext<DbRaisa>(str =>
{
    str.UseSqlServer(builder.Configuration.GetConnectionString("RaisasString"));
});

var app = builder.Build();
//interface da servicebis dakavshireba


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
