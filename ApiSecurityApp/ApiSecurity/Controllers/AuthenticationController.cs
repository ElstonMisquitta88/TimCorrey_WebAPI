using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    public IConfiguration _config { get; set; }

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }


    public record AuthenticationData(string? Username, string? Password);
    public record UserData(int UserID, string UserName,string Title,string EmployeeID);



    //POST :  api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var User = ValidateCredentials(data);
        if (User == null)
        {
            return Unauthorized();
        }

        // Generate Token
        var Token =GenerateToken (User);
        return Ok(Token);

    }



    private string GenerateToken(UserData user)
    {
        // Generate Value
        var secretkey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey"))
            );

        // Sign the Token
        var signingCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

        // Add Claims (Standard Defined Claims)
        List<Claim> Claims = new();
        Claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserID.ToString()));
        Claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName.ToString()));


        //Add Custom Claims
        Claims.Add(new("title",user.Title ));



        // Token Creation
        var Token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            Claims,
            DateTime.UtcNow, // When Token will be Valid
            DateTime.UtcNow.AddMinutes(1), // When Token will Expire
            signingCredentials
            );

        return new JwtSecurityTokenHandler ().WriteToken(Token);
    }
    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // TODO:  Demo Code for Validation

        //User 1 (Sample)
        if (CompareValue(data.Username, "admin")
            && CompareValue(data.Password, "admin123"))
        {
            return new UserData(1, data.Username!,"Business Owner","E001");
        }

        //User 2 (Sample)
        if (CompareValue(data.Username, "elston")
            && CompareValue(data.Password, "elston123"))
        {
            return new UserData(2, data.Username!, "Employee", "E002");
        }

        return null;
    }
    private bool CompareValue(string? actual, string expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }


}

