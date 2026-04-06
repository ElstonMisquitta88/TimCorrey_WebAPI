using Microsoft.FeatureManagement;
using Serilog;
using TodoApi.StartupConfig;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Custom Services --------------------
builder.AddStandardServices();
builder.AddCustomServices();
builder.AddHealthCheckServices();
builder.AddAuthCheckServices();

// Feature flags
builder.Services.AddFeatureManagement();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod();
    });
});

// -------------------- Serilog Configuration (FINAL) --------------------
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "todo-logs-{0:yyyy.MM.dd}"
    });
});

// -------------------- Build App --------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// -------------------- Middleware --------------------

// Request timing middleware
app.Use(async (context, next) =>
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();


await next();

    stopwatch.Stop();

    Log.Information("Request {Method} {Path} took {Elapsed} ms",
        context.Request.Method,
        context.Request.Path,
        stopwatch.ElapsedMilliseconds);


});

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

// Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health checks
app.MapHealthChecks("/health").AllowAnonymous();

app.Run();
