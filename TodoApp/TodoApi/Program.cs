using TodoApi.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

//[+] Custom Service
builder.AddStandardServices();
builder.AddCustomServices();
builder.AddHealthCheckServices();
builder.AddAuthCheckServices();
//[-] Custom Service

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
