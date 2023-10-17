using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PashaBankApp.Controllers.CommandHandler.BonusHandler;
using PashaBankApp.Controllers.CommandHandler.DistributorHandler;
using PashaBankApp.Controllers.CommandHandler.DistributorSaleHandler;
using PashaBankApp.Controllers.CommandHandler.ErrorHandler;
using PashaBankApp.Controllers.CommandHandler.LogHandler;
using PashaBankApp.Controllers.CommandHandler.ProductHandler;
using PashaBankApp.Controllers.Interface;
using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;
using PashaBankApp.Services;
using PashaBankApp.Services.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pasha Bank", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please enter Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        }, new List<string>() }
    });
});
builder.Services.AddScoped<IDistributor, DistributorServices>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IDistributorSale,DistributorSaleServices>();
builder.Services.AddScoped<IError, ErrorServices>();
builder.Services.AddScoped<ILog,LogServices>();
builder.Services.AddScoped<Ibonus, BonusServices>();
builder.Services.AddScoped<IManager, ManagerServices>();

//errorhandle
builder.Services.AddTransient<IcommandHandlerList<Error>, AllErrorCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<ErrorBetweenDateRequest, Error>, ErrorBetweenDateCommandHandler>();

//loghandle
builder.Services.AddTransient<IcommandHandlerList<Log>, AllLogCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<LogBetweenDateRequest, Log>, LogBetweenDateCommandHandler>();

//producthandle
builder.Services.AddTransient<ICommandHandler<InsertProducts>, InsertProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateProduct>, UpdateProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteProducts>, DeleteProductCommandHandler>();
builder.Services.AddTransient<ICommandHandler<SoftDeleteProductRequest>, SoftDeleteProductCommandHandler>();
builder.Services.AddTransient<IcommandHandlerList<GetProductResponse>, GetAllProductCommandHandler>();

//distributorsaleHandler
builder.Services.AddTransient<ICommandHandler<InsertDistributorSaleRequest>, InsertDistributorSaleCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteDistributorSale>, DeleteDistributorSaleCommandHandler>();
builder.Services.AddTransient<ICommandHandler<SoftDeleteDistributorSaleRequest>, SoftDeleteDistributorSaleCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<GetDistributorSaleRequest,DistributorSale>, DistributorSaleGetDistCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<DistributorSaleGetDateRequest,DistributorSale>, DistributorSaleGetDateCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<DistributorSaleGetProductRequest,DistributorSale>,DistributorSaleGetProductCommandHandler>();

//distributorHandler
builder.Services.AddTransient<ICommandHandler<InsertDistributorRequest>, InsertDistributorCommandHandler>();
builder.Services.AddTransient<ICommandHandler<UpdateDistributorRequest>, UpdateDistributorCommandHandler>();
builder.Services.AddTransient<ICommandHandler<DeleteDistributor>, DeleteDistributorCommandHandler>();
builder.Services.AddTransient<ICommandHandler<SoftDeleteDistributor>, SoftDeleteDistributorCommandHandler>();
builder.Services.AddTransient<IcommandHandlerList<GetDistributor>, AllDistributorCommandHandler>();

//bonusHandler
builder.Services.AddTransient<ICommandHandler<InsertBonus>, InsertBonusCommandHandler>();
builder.Services.AddTransient<IcommandHandlerListAndResponse<GetBonus, SortBonus>, GetBonusBySurnameCommandHandler>();
builder.Services.AddTransient<IcommandHandlerList<SortBonusAsc>, SortBonusAscCommandHandler>();
builder.Services.AddTransient<IcommandHandlerList<SortBonus>, SortBonusDescCommandHandler>();


builder.Services.AddDbContext<DbRaisa>(str =>
{
    str.UseSqlServer(builder.Configuration.GetConnectionString("RaisasString"));
});

builder.Services.AddIdentity<Manager, IdentityRole>(io =>
{
    io.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<DbRaisa>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(ops =>
{
    ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(ops =>
{
    ops.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:key").Value)),
    };
});


builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy("ManagerOnly", policy => policy.RequireRole("MANAGER"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
