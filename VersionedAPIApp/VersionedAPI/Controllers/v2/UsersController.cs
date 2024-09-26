using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VersionedAPI.Controllers.v2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class UsersController : ControllerBase
{
    // GET: api/v2/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Version 2 : value1", "Version 2 : value2" };
    }

}
