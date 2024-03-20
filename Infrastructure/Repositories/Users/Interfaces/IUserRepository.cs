using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<ApplicationUser> UpdateAsync(ApplicationUser user);
        public Task<bool> DeactivateAsync(Guid id);
        public Task<ApplicationUser> GetByIdAsync(Guid id);
        public Task<ApplicationUser> GetUserByUsername(string username);
        public Task<ApplicationUser> GetUserByEmail(string email);
        public Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        public Task<IEnumerable<ApplicationUser>> GetUsersWithRole(Guid roleId);
        public Task<IEnumerable<string>> GetUserRoles(Guid userId);
        // Prioritize usage of role name over role id for performance reasons
        public Task<bool> AddUserToRole(ApplicationUser user, Guid roleId);
        public Task<bool> AddUserToRole(ApplicationUser user, string roleName);
        public Task<bool> RemoveUserRole(Guid user, string roleName);
        public Task<bool> RemoveUserRoles(Guid user, Guid roleId);
    }
}