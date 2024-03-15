using Infrastructure.Entities.Users;

namespace Infrastructure.Repositories.Users.Interfaces
{
    public interface IPermissionsRepository
    {
        public Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<string> roleNames);
        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId);
        public Task<IEnumerable<ApplicationRole>> GetRolesWithPermission(Guid permissionId);
        public Task<IEnumerable<Permission>> GetPermissions();
        public Task<IEnumerable<Permission>> GetPermissions(string system);
        public Task<Permission> GetPermission(Guid id);
        public Task<Permission> CreatePermission(Permission permission);
        public Task<Permission> UpdatePermission(Permission permission);
        public bool DeletePermission(Guid id);

    }
}
