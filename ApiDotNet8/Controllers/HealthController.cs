using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet8.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("OK");
    }
}