using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestJwtAuthController : ControllerBase
    {
        [HttpGet("havejwt")]
        [Authorize]
        public async Task<IActionResult> TestJwt()
        {
                return Ok("Successfull!");
        }
        [HttpGet("nojwt")]
        public async Task<IActionResult> TestNoJwt()
        {
            return Ok("Successfull Because It haven't Authorize!");
        }
    }
}
