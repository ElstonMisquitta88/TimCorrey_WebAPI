using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VersionedAPI.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    // GET: api/v1/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Version 1 : value1", "Version 1 : value2" };
    }


}
