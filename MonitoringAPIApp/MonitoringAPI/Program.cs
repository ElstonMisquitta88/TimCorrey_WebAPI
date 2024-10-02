//https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
//https://github.com/IzyPro/WatchDog


// [+] Health Checks
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MonitoringAPI.Controllers.HealthChecks;
// [-] Health Checks

// Default : https://localhost:7035/health

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [+] Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site Health Check")
    .AddCheck<RandomHealthCheck>("Database Health Check");

// UI Display Part  :  https://localhost:7035/healthchecks-ui
builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health");
    opts.SetEvaluationTimeInSeconds(5);
    opts.SetMinimumSecondsBetweenFailureNotifications(10);
}).AddInMemoryStorage();

// [-] Health Checks


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

// [+] Health Checks
app.MapHealthChecks("/health",new HealthCheckOptions
{
    ResponseWriter =UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
// [-] Health Checks

app.Run();
