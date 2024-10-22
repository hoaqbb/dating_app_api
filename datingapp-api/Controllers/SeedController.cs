using API.Data;
using datingapp_api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace datingapp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly DatingAppContext _context;
        private readonly IServiceProvider _services;

        public SeedController(DatingAppContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> ImportData()
        {
            var context = _services.GetRequiredService<DatingAppContext>();
            var userManager = _services.GetRequiredService<UserManager<User>>();
            var roleManager = _services.GetRequiredService<RoleManager<Role>>();
            await context.Database.MigrateAsync();
            var check = await Seed.SeedUsers(userManager, roleManager);
            if (check) return Ok("ok");
            return BadRequest("false");
        }
    }
}
