using AutoMapper;
using Mango.Services.ShoppingCartAPI;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Extensions;
using Mango.Services.ShoppingCartAPI.Startup;
using Mango.StringLocalized;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Connect to database SQL SERVER

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




#region Automapper

builder.Services.RegisterAutoMapperServices();

#endregion

#region HttpClients

builder.Services.AddHttpClient("Product", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]!);
}).SetHandlerLifetime(TimeSpan.FromMinutes(60));

// thử nghiệm set timeout

builder.Services.AddHttpClient("Coupon", x =>
    x.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]!)).SetHandlerLifetime(TimeSpan.FromMinutes(60));
#endregion 

#region API services
builder.Services.RegisterAPIServices();
#endregion

#region Localized

builder.Services.ConfigureLocalizationServices();

#endregion

// add token swagger gen
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: 'Bearer Generated-JWT-Token-Long'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
                  new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});


// add authenticate
builder.AddAppAuthentication();

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApplyMigration();
app.Run();


void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (dbContext.Database.GetPendingMigrations().Count() > 0)
        {
            dbContext.Database.Migrate();
        }
    }
}




