using API.Data;
using datingapp_api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace datingapp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly DatingAppContext _context;

        public SeedController(DatingAppContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ImportData()
        {
            var check = await Seed.SeedUsers(_context);
            if (check) return Ok("ok");
            return BadRequest("false");
        }
    }
}
