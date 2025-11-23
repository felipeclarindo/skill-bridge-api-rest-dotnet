using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiDescriptionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetApiDescription()
        {
            var info = new ApiDescription
            {
                Name = "SkillBridge API",
                Version = "1.0.0",
                Status = "Online",
                Desenvolvedor = "Felipe Clarindo",
                Github = "https://github.com/felipeclarindo",
                Environment =
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                Timestamp = DateTime.UtcNow,
            };

            return Ok(info);
        }
    }
}
