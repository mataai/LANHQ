using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IRolesRepository
    {
        public Task<IEnumerable<ApplicationRole>> GetRoles();
        public Task<ApplicationRole> GetRole(Guid id);
        public Task<ApplicationRole> CreateRole(ApplicationRole role);
        public Task<ApplicationRole> UpdateRole(ApplicationRole role);
        public bool DeleteRole(Guid id);
        public Task<bool> AddUserToRole(ApplicationUser user, string roleName);
        public Task<bool> CreateRole(string roleName);
        public Task<bool> DeleteRole(string roleName);
        public Task<ApplicationRole> GetRoleById(Guid roleId);
        public Task<ApplicationRole> GetRoleByName(string roleName);
        public Task<IEnumerable<ApplicationUser>> GetUsersInRole(string roleName);
        public Task<IEnumerable<ApplicationUser>> GetUsersNotInRole(string roleName);
        public Task<bool> RemoveAllRolesFromUser(ApplicationUser user);
        public Task<bool> RemoveUserFromRole(ApplicationUser user, string roleName);
        public Task<bool> RemoveUsersFromRole(List<ApplicationUser> users, string roleName);
        public Task<bool> RoleExists(string roleName);
        public Task<bool> UpdateRole(string roleName, string newRoleName);
    }
}
