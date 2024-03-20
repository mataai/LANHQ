using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users.Internal
{

    public class RolesRepository(LANHQDbContext context, RoleManager<ApplicationRole> roleManager) : IRolesRepository
    {
        private readonly LANHQDbContext _context = context;
        // private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public Task<List<ApplicationRole>> GetRoles() => _context.Roles.ToListAsync();

        public async Task<ApplicationRole> GetRoleById(Guid roleId)
        {
            return await _roleManager.FindByIdAsync(roleId.ToString()) ?? throw new Exception("Role not found");

        }

        public async Task<ApplicationRole> GetRoleByName(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName) ?? throw new Exception("Role not found");
        }

        public async Task<ApplicationRole> CreateRole(string roleName, string description)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole(roleName, description));
            if (!result.Succeeded)
            {
                // TODO error handling
                throw new Exception("Role not created");
            }
            else
            {
                return await _roleManager.FindByNameAsync(roleName) ?? throw new Exception("Role not found");
            }
        }

        public async Task<bool> UpdateRole(string roleName, string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName) ?? throw new Exception("Role not found");
            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<ApplicationRole> GetRole(Guid id)
        {
            return await _context.Roles.FindAsync(id) ?? throw new Exception("Role not found");
        }

        public Task<ApplicationRole> UpdateRole(ApplicationRole role)
        {
            var result = _context.Roles.Update(role);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
            }
            else
            {
                // TODO: throw custom exception
                throw new Exception("404 Role not found");
            }
        }

    }
}
