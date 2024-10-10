//[+] Rate Limit
using AspNetCoreRateLimit;
using ApiProtection.StartupConfig;
//[-] Rate Limit

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//[+] Caching
builder.Services.AddResponseCaching();
//[-] Caching


//[+] Rate Limit
builder.Services.AddMemoryCache();  // Used to Cache Call Information

/*
 * This would not work if you have multiple servers behind a load Balancer. This config is per Server wise.
 * Alternative is to use Redis Cache
 */

builder.AddRateLimitServices();  // Moved to A Custom Extension Method


//[-] Rate Limit




var app = builder.Build(); // Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//[+] Caching
app.UseResponseCaching();
//[-] Caching

app.UseAuthorization();
app.MapControllers();

//[+] Rate Limit
app.UseIpRateLimiting();
//[-] Rate Limit

app.Run();
