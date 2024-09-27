using Asp.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//[+] Configer Swagger UI Section for Version Logic
builder.Services.AddSwaggerGen(opts => 
{
    //(A) Variables
    var title = "Our Versioned API";
    var description = "API Documentation";
    var terms = new Uri("https://localhost:7299/terms");
    var license = new OpenApiLicense()
    {
        Name = "This is my Full liscence Information"
    };
    var contact = new OpenApiContact()
    {
        Name="Elston",
        Email="elston007@gmailcom",
        Url =new Uri ("https://courses.iamtimcorey.com/")
    };

    // (B) Set Values
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{title} v1",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });


    opts.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

});

//[-] Configer Swagger UI Section for Version Logic



//[+] Version Logic
// Refer - https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
builder.Services.AddApiVersioning(
    options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
})
.AddApiExplorer(opts =>   //[+] For Swagger Configuration
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl = true;
})
.AddMvc(); // This is needed for controllers (IMP)
//[-] Version Logic









var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //[+] Configer Swagger UI Section for Version Logic
    app.UseSwaggerUI( opts => 
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
    //[-] Configer Swagger UI Section for Version Logic

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
