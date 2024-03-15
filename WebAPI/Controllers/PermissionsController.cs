using Infrastructure;
using Infrastructure.DTO.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {

        private LANHQDbContext _context;

        public PermissionsController(LANHQDbContext context)
        {
            _context = context;
        }

        // GET: api/Permissions/Roles
        [HttpGet("Roles")]
        public IActionResult GetRoles()
        {
            // TODO: Implement your logic to retrieve roles
            return Ok(_context.Roles); // Return an empty list or your roles list
        }

        // POST: api/Permissions/Roles
        [HttpPost("Roles")]
        public IActionResult CreateRole([FromBody] CreateRoleDTO role)
        {
            // TODO: Implement your logic to create a role
            EntityEntry<IdentityRole> result = _context.Roles.Add(new IdentityRole(role.Name));
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRoles), result.Entity);
        }

        // PUT: api/Permissions/Roles/5
        [HttpPut("Roles/{id}")]
        public IActionResult UpdateRole(int id, [FromBody] string role)
        {
            // TODO: Implement your logic to update a role by id
            return NoContent();
        }

        // DELETE: api/Permissions/Roles/5
        [HttpDelete("Roles/{id}")]
        public IActionResult DeleteRole(int id)
        {
            // TODO: Implement your logic to delete a role by id
            return NoContent();
        }

        // GET: api/Permissions/UserClaims
        [HttpGet("UserClaims")]
        public IActionResult GetUserClaims(string userId)
        {
            // TODO: Implement your logic to retrieve claims
            return Ok(_context.UserClaims.Where(user => user.Id.Equals(userId))); // Return an empty list or your claims list
        }

        // GET: api/Permissions/RoleClaims
        [HttpGet("RoleClaims")]
        public IActionResult GetRoleClaims(string roleId)
        {
            // TODO: Implement your logic to retrieve claims
            return Ok(_context.RoleClaims.Where(user => user.Id.Equals(roleId))); // Return an empty list or your claims list
        }

        [HttpDelete("Claims/{id}")]
        public IActionResult DeleteClaim(int id)
        {
            // TODO: Implement your logic to delete a claim by id
            return NoContent();
        }
    }
}