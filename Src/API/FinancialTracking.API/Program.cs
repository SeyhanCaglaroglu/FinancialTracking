using FinancialTracking.API.Extensions;
using FinancialTracking.Application.Contracts.Caching;
using FinancialTracking.Application.Extensions;
using FinancialTracking.Auth.Extensions;
using FinancialTracking.Auth.Options;
using FinancialTracking.Caching;
using FinancialTracking.Persistence.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithValidation().AddAppServices(builder.Configuration).AddCustomIdentity().AddVersioningExt().AddExceptionHandlerExt();

builder.Services.AddSingleton<IRedisService, RedisService>();

// TokenOptions al
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

builder.Services.AddApplication(builder.Configuration)
                    .AddPersistence(builder.Configuration)
                    .AddAuthExt(tokenOptions);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
