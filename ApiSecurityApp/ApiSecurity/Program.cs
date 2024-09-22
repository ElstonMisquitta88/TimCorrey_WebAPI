using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiSecurity.Constants;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(opts =>
    {
        // (a) Claims Policy - Must Have Employee ID in Claims
        opts.AddPolicy(PolicyConstants.MustHaveEmployeeID, policy =>
        {
            policy.RequireClaim("employeeid");
        });


        // (b) Claims Policy Based on Title - MustBetheOwner ( Like User Role Eg : Admin/BackOffice/Risk/Developer )
        opts.AddPolicy(PolicyConstants.MustBetheOwner , policy =>
        {
            policy.RequireClaim("Title", "Business Owner");
        });




        // Default Authentication for all Controllers 
        opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    }
   );


// Token Based
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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
