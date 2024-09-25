using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VersionedAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]

    public class UsersController : ControllerBase
    {
        // GET: api/v1/Users
        // GET: api/v2/Users

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<UsersController>/5
        //[HttpGet("FetchSpecific/{id}")]
        //public string Get(int id)
        //{
        //    return "value of Version 3";
        //}
    }
}
