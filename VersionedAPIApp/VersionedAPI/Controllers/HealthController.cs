using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersionedAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersionNeutral]
public class HealthController : ControllerBase
{
    [HttpGet]
    [Route("PingCheck")]

    public IActionResult ping()
    {
        // TODO : Database call check
        // TODO : Cache Check

        return Ok("Up & Running API");
    }

}
