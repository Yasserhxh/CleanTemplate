using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DinnersController : ApiController
    {
        [HttpGet]
        public IActionResult GetDinner()
        {
            return Ok(Array.Empty<string>());
        }
    }
}

