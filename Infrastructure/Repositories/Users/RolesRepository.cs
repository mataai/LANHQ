using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class RolesRepository : IRolesRepository
    {
        private readonly LANHQDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public RolesRepository(LANHQDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationRole>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ApplicationRole?> GetRoleById(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<ApplicationRole?> GetRoleByName(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                // TODO error handling
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
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

        public async Task<bool> AddUserToRole(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserFromRole(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            await _roleManager.CreateAsync(new ApplicationRole(roleName));
            return true;
        }
        public async Task<bool> UpdateRole(string roleName, string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                // TODO error handling
                return false;
            }
            role.Name = newRoleName;
            await _roleManager.UpdateAsync(role);
            return true;
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                // TODO error handling
                return false;
            }
            await _roleManager.DeleteAsync(role);
            return true;
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
         
    }
}
