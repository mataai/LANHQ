using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> DeactivateAsync(Guid id);
        public Task<ApplicationUser> UpdateAsync(ApplicationUser user);
        public Task<ApplicationUser> GetByIdAsync(Guid id);
        public Task<ApplicationUser> GetUserByUsername(string username);
        public Task<ApplicationUser> GetUserByEmail(string email);
        public Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId);
        public Task<IEnumerable<ApplicationUser>> GetUsersWithRole(Guid roleId);
        public Task<IEnumerable<string>> GetRolesForUser(Guid userId);
        public Task<IEnumerable<string>> GetRolesForUser(string username);

    }
}