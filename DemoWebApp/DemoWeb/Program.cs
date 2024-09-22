using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddControllers();  // Added by Elston (for Adding API in Web App)
builder.Services.AddEndpointsApiExplorer(); // Added by Elston (for Adding API in Web App)
builder.Services.AddSwaggerGen(); // Added by Elston (for Adding API in Web App)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}
else
{
    app.UseSwagger(); // Added by Elston (for Adding API in Web App)
    app.UseSwaggerUI(); // Added by Elston (for Adding API in Web App)
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers(); // Added by Elston (for Adding API in Web App)
app.Run();
