using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SavorChef.Backend.Controllers;

public class TestController : ControllerBase
{
    [Authorize]
    [Route("test")]
    [HttpGet]
    public IActionResult Test()
    {
        return new OkObjectResult("Works!");
    }
}