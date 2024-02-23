using Infrastructure;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IdentityDbContext _context;
        public UsersController(IdentityDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public List<ApplicationUser> Get()
        {
            return _context.Users.ToList();
        }
    }
}
