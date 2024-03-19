using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IRolesRepository
    {
        public Task<List<ApplicationRole>> GetRoles();
        public Task<ApplicationRole> GetRole(Guid id);
        public Task<ApplicationRole> CreateRole(string roleName, string description);
        public Task<ApplicationRole> UpdateRole(ApplicationRole role);
        public Task<bool> DeleteRole(Guid id);
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
