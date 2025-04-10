using Microsoft.AspNetCore.Mvc;

namespace SwaggerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/<Users>

    /// <summary>
    /// Gets a list of all the users in the System.
    /// </summary>
    /// <remarks>
    /// Sample Request would be GET/Users
    /// Sample Response would be
    /// [
    ///     {
    ///         "id":1,
    ///         "name" :"Elston"
    ///     },
    ///     {
    ///         "id":2,
    ///         "name" :"Tim"
    ///     }
    /// ]
    /// </remarks>
    /// <returns>A list of users</returns>
    [HttpGet("GetCustomAllData")]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    //// GET api/<UsersController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST api/<UsersController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<UsersController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<UsersController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
