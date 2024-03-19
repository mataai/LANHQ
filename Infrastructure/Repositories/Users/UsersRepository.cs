using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly LANHQDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(LANHQDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<bool> DeactivateAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddYears(100);
                var result = await _context.SaveChangesAsync();
                return result >= 1;
            }
            return false;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                // TODO: throw custom exception
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<ApplicationUser> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUsersWithRole(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddUserToRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if(!result.Succeeded)
            {
                // TODO: throw custom exception
                throw new Exception("Error adding user to role");
            }
            return result.Succeeded;
        }

        public async Task<bool> RemoveUserFromRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if(!result.Succeeded)
            {
                // TODO: throw custom exception
                throw new Exception("Error removing user from role");
            }
            return result.Succeeded;
        }

        public async Task<bool> RemoveAllRolesFromUser(ApplicationUser user)
        {
            // TODO: Manually log changes 
            var result = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ExecuteDeleteAsync();
            return result >= 1;
        }

        public Task<bool> RemoveUsersFromRole(List<ApplicationUser> users, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
