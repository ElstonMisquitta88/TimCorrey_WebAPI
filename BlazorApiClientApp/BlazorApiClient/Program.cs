using BlazorApiClient.Logic;
using BlazorApiClient.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient("api", opts =>
{
    opts.BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIUrl"));
});
/*

Singleton - One instance of a resource, reused anytime it's requested.

Scoped - One instance of a resource, but only for the current request. New request (i.e. hit an API endpoint again) = new instance

Transient - A different instance of a resource, everytime it's requested.
 */


builder.Services.AddScoped<TokenModel>();
builder.Services.AddScoped<IDemoLogic,DemoLogic>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
