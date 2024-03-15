using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IUserRepository
    {
        //simple crud actions
        public Task<ApplicationUser> GetUserById(Guid id);
        public Task<ApplicationUser> GetUserByUsername(string username);
        public Task<ApplicationUser> GetUserByEmail(string email);
        public Task<ApplicationUser> UpdateUser(ApplicationUser user);
        public Task<ApplicationUser> DeactivateUser(Guid id);
        public Task<IEnumerable<ApplicationUser>> GetUsers();
        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId);
        public Task<IEnumerable<ApplicationUser>> GetUsersWithRole(Guid roleId);
        public Task<IEnumerable<string>> GetRolesForUser(Guid userId);
        public Task<IEnumerable<string>> GetRolesForUser(string username);
        
    }
}