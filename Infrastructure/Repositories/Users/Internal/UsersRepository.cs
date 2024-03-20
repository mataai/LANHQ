using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users.Internal
{
    public class UserRepository(LANHQDbContext context, UserManager<ApplicationUser> userManager) : IUserRepository
    {
        private readonly LANHQDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

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

        public Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return Task.FromResult(_context.Users.AsEnumerable());
        }

        public Task<ApplicationUser> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_context.Users.Include(x => x.Permissions).FirstOrDefault(x => x.Id == id) ?? throw new Exception("User not found"));
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

        public Task<IEnumerable<ApplicationUser>> GetUsersWithRole(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetUserRoles(Guid userId)
        {
            var data = await _userManager.GetRolesAsync(new ApplicationUser { Id = userId });
            return data;
        }

        public async Task<bool> AddUserRoles(ApplicationUser user, IEnumerable<Guid> roleId)
        {
            _context.UserRoles.AddRange(roleId.Select(x => new IdentityUserRole<Guid> { UserId = user.Id, RoleId = x }));
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveUserRoles(Guid userId, IEnumerable<Guid> rolesDTO)
        {
            _context.UserRoles.RemoveRange(rolesDTO.Select(x => new IdentityUserRole<Guid> { UserId = userId, RoleId = x }));
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddUserPermissions(ApplicationUser user, IEnumerable<Guid> permissionId)
        {
            var result = _context.Users.Update(new ApplicationUser { Id = user.Id, Permissions = permissionId.Select(x => new Permission { Id = x }).ToList() });
            return await _context.SaveChangesAsync() > 0;

        }
        public async Task<bool> RemoveUserPermissions(Guid user, IEnumerable<Guid> permissionId)
        {
            throw new NotImplementedException();
        }
    }
}
