using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{

    public class RolesRepository(LANHQDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : IRolesRepository
    {
        private readonly LANHQDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

        public Task<List<ApplicationRole>> GetRoles() => _context.Roles.ToListAsync();

        public async Task<ApplicationRole> GetRoleById(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId) ?? throw new Exception("Role not found");

        }

        public async Task<ApplicationRole> GetRoleByName(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName) ?? throw new Exception("Role not found");
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInRole(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersNotInRole(string roleName)
        {
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return users.Except(usersInRole);
        }

        public Task<bool> RemoveAllRolesFromUser(ApplicationUser user)
        {
            var roles = _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                // TODO error handling
                return Task.FromResult(false);
            }
            var result = _userManager.RemoveFromRolesAsync(user, roles.Result);
            return Task.FromResult(result.Result.Succeeded);

        }

        public async Task<bool> RemoveUserFromRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            // todo no success

            return result.Succeeded;
        }

        public async Task<bool> RemoveUsersFromRole(List<ApplicationUser> users, string roleName)
        {
            foreach (ApplicationUser user in users)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
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
