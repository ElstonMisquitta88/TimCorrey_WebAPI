using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    public record AuthenticationData(string? UserName, string? Password);

    public record UserData(int Id, string FirstName, string LastName, string UserName);



    [HttpPost("Token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);
        if (user == null)
        {
            return Unauthorized();
        }
        string Token = GenerateToken(user);
        return Ok(Token);
    }

    private string GenerateToken(UserData user)
    {
        // (1) Generate secretKey Value
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
            _config.GetValue<string>("Authentication:SecretKey")));

        // (2) Sign the Token
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


        // (3) Add Claims 
        List<Claim> Claims = new();
        Claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        Claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        Claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
        Claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));


        // (4) Token Creation
        var Token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            Claims,
            DateTime.UtcNow, // When Token will be Valid
            DateTime.UtcNow.AddMinutes(1), // When Token will Expire
            signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(Token);
    }





    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // Non Production Code
        if (CompareValues(data.UserName, "admin") && CompareValues(data.Password, "admin"))
        {
            return new UserData(1, "Elston", "Misquitta", data.UserName!);
        }
        if (CompareValues(data.UserName, "sstorm") && CompareValues(data.Password, "admin"))
        {
            return new UserData(2, "Sue", "Storm", data.UserName!);
        }
        return null;
    }

    private bool CompareValues(string? actual, string? expected)
    {
        if (actual is not null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }

}
