using ApiProtection.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProtection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //[+] Rate Limiting
        // GET: api/FetchDemoUser
        [HttpGet("FetchDemoUser")]
        public IEnumerable<string> Get_FetchDemoUser()
        {
            return new string[] { Random.Shared.Next(1, 101).ToString() };
        }
        //[-] Rate Limiting




        //[+] Caching
        // GET: api/Users
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any,NoStore =false)]
        public IEnumerable<string> Get()
        {
            return new string[] { Random.Shared.Next(1, 101).ToString() };
        }
        //[-] Caching



        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (ModelState.IsValid)
            {
                return Ok("Model was Valid");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



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
}
