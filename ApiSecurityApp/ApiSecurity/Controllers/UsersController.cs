﻿using ApiSecurity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    public IConfiguration _config { get; }

    public UsersController(IConfiguration config)
    {
        _config = config;
    }



    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }



    // GET : api/Users/5
    [HttpGet("{id}")]
    [Authorize (Policy = PolicyConstants.MustHaveEmployeeID)]
    [Authorize(Policy = PolicyConstants.MustBetheOwner)]
    public string Get(int id)
    {
        return "value";
    }






    //// GET: api/Users/GetConnectionString
    //[HttpGet("GetConnectionString")]
    //public string GetConnectionString()
    //{
    //    return _config.GetConnectionString("Default");
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
