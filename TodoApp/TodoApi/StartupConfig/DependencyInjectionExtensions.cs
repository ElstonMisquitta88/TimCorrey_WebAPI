using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoLibrary.DataAccess;

namespace TodoApi.StartupConfig;

public static class DependencyInjectionExtensions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
   
    }
    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        // [+] Dependency Injection
        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        builder.Services.AddSingleton<ITodoData, TodoData>();
        // [-] Dependency Injection

    }
    public static void AddHealthCheckServices(this WebApplicationBuilder builder)
    {
        //[+] Health Checks - SQL
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("Default")!);
        //[-] Health Checks

    }
    public static void AddAuthCheckServices(this WebApplicationBuilder builder)
    {
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
    }
}
