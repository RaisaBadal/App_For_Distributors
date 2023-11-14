using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PashaBankApp.DbContexti;
using PashaBankApp.Models;
using PBG.Distributor.UI.Settings;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

#region SwaggenGen

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
#endregion

#region AutomaticInject
builder.Services.AddInjectServices(Assembly.GetExecutingAssembly());
builder.Services.AddInjectRep(Assembly.GetExecutingAssembly());
builder.Services.AddInjectHandlers(Assembly.GetExecutingAssembly());
#endregion

builder.Services.AddDbContext<DbRaisa>(str =>
{
    str.UseSqlServer(builder.Configuration.GetConnectionString("RaisasString"));
});

builder.Services.AddIdentity<Manager, IdentityRole>(io =>
{
    io.Password.RequiredLength = 5;
}).AddEntityFrameworkStores<DbRaisa>().AddDefaultTokenProviders();

#region Authentification
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
#endregion


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
