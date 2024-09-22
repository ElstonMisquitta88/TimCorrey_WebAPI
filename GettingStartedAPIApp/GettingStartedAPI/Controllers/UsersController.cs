using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GettingStartedAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/Users/GetAllUsers
    [HttpGet("GetAllUsers")]
    public List<string> GetAllUsers()
    {
       List<string> users = new List<string>();
        for (int i = 0; i < Random .Shared.Next(2,10); i++)
        {
            users.Add("Value :" + i.ToString());
        }
        return users;
    }


    // GET: api/Users/GetAllUsers_V1
    [HttpGet("GetAllUsers_V1")]
    public IEnumerable<string> GetAllUsers1()
    {
        return new string[] { "value1", "value2", "value3" };
    }


    // GET: api/Users/FetchSpecific/5
    [HttpGet("FetchSpecific/{id}")]
    public string Get(int id)
    {
        return "value one is " + id.ToString ();
    }






    // POST api/Users
    [HttpPost]
    public string Post([FromBody] string value)
    {
        return value + " Hello from API";
    }


    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }



    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
