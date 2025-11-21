using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiDescriptionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetApiDescription()
        {
            var info = new
            {
                name = "SkillBridge API",
                version = "1.0.0",
                status = "Online",
                desenvolvedor = "Felipe Clarindo",
                github = "https://github.com/felipeclarindo",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                timestamp = DateTime.UtcNow
            };

            return Ok(info);
        }
    }
}
