//https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
//https://github.com/IzyPro/WatchDog


// Default : https://localhost:7035/health
// UI Display Part  :  https://localhost:7035/healthchecks-ui




// [+] Health Checks
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MonitoringAPI.Controllers.HealthChecks;
using WatchDog;
// [-] Health Checks

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [+] Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site Health Check")
    .AddCheck<RandomHealthCheck>("Database Health Check");


// [+] For WatchDog Specific
builder.Services.AddWatchDogServices();
// [-] For WatchDog Specific



// UI Display Part  :  https://localhost:7035/healthchecks-ui
builder.Services.AddHealthChecksUI(opts =>
{
    opts.AddHealthCheckEndpoint("api", "/health");
    opts.SetEvaluationTimeInSeconds(5);
    opts.SetMinimumSecondsBetweenFailureNotifications(10);
}).AddInMemoryStorage();

// [-] Health Checks






var app = builder.Build();


// [+] For WatchDog Specific
app.UseWatchDogExceptionLogger();
// [-] For WatchDog Specific



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
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
// [-] Health Checks


// [+] For WatchDog Specific
app.UseWatchDog(opts =>
{
    opts.WatchPageUsername = app.Configuration.GetValue<string>("WatchDog:UserName");
    opts.WatchPagePassword= app.Configuration.GetValue<string>("WatchDog:Password");
    opts.Blacklist = "health";
});
// [-] For WatchDog Specific


app.Run();
