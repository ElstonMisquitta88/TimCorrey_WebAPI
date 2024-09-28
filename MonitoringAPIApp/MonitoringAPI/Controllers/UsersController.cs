using Microsoft.AspNetCore.Mvc;

namespace MonitoringAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{


    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }



    // GET api/Users/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if (id < 0 || id > 100)
        {
            //[+] Structured Error Logging
            _logger.LogWarning("The Id Used {Id} was invalid", id);
            //[-] Structured Error Logging

            return BadRequest("The Index was out of Range");
        }

        _logger.LogInformation("The api\\Users\\{ID} was called", id);
        return Ok($"Value entered is {id}");
    }

    // GET api/Users/GetV1/5
    [HttpGet("GetV1/{id}")]
    public IActionResult GetV1(int id)
    {
        try
        {
            if (id < 0 || id > 100)
            {
                throw new ArgumentOutOfRangeException (nameof (id));
            }

            _logger.LogInformation("The api\\Users\\{ID} was called", id);
            return Ok($"Value entered is {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The Id Used {Id} was invalid", id);
            return BadRequest("The Index was out of Range");
        }
    }



    //// GET: api/Users
    //[HttpGet]
    //public IEnumerable<string> Get()
    //{
    //    return new string[] { "value1", "value2" };
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
