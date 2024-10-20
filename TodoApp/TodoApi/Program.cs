using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoLibrary.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[+] Authentication
builder.Services.AddAuthorization(opts =>
{
    // Default Authentication for all Controllers 
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                 builder.Configuration.GetValue<string>("Authentication:SecretKey")
                ))
        };
    });
//[-] Authentication

//[+] Health Checks - SQL
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("Default")!);
//[-] Health Checks

// Dependency Injection
builder.Services.AddSingleton<ISqlDataAccess,SqlDataAccess>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//[+] Authentication
app.UseAuthentication();
//[-] Authentication

app.UseAuthorization();
app.MapControllers();

// [+] Health Checks
app.MapHealthChecks("/health").AllowAnonymous();
// [-] Health Checks

app.Run();
