using Core.Attributes;
using Infrastructure;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [PermissionAuthorize]
    [Route("api/[controller]")]
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

        [HttpPut("deactivate")]
        //[Authorize(Roles = "Admin")]
        public ActionResult Put(String id)
        {
            //ApplicationUser user = _context.Users.FirstOrDefault(x => x.Id == id);

            //if (user == null)
            //{
            //    throw new Exception();
            //}

            //user.LockoutEnabled = true;

            User.IsInRole("admin");
            User.Claims.Any(claim => claim.Type == "Test");

            return new JsonResult(HttpContext.User?.Identity);
        }
    }
}
