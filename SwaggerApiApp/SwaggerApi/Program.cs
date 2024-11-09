using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Custom API Title",
        Description = "Desc about API",
        TermsOfService=new Uri("https://courses.iamtimcorey.com/"),
        Contact=new OpenApiContact
        {
            Name="Elston",
             Url= new Uri("https://courses.iamtimcorey.com/"),
             Email="elston@yahoo.com"
        },
        License=new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://courses.iamtimcorey.com/")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opts =>
    {
        //opts.SerializeAsV2 = true;  // Setting the API as Open API Version 2
    });
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); //Changes for Swagger at the root
        opts.RoutePrefix = string.Empty; //Changes for Swagger at the root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
