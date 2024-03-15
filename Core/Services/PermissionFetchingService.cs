using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users;

namespace Core.Services
{
    public class PermissionFetchingService
    {
        private PermissionsRepository _permissionsRepo;
        private RolesRepository _rolesRepo;
        public PermissionFetchingService(PermissionsRepository permissionsRepo, RolesRepository rolesRepo)
        {
            this._permissionsRepo = permissionsRepo;
            this._rolesRepo = rolesRepo;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId)
        {
            var userRoles = await _rolesRepo.GetRolesForUser(userId); 
            var permissions = new List<Permission>();
            permissions.AddRange(await _permissionsRepo.GetPermissionsForUser(userId));
            permissions.AddRange(await _permissionsRepo.GetPermissionsForRoles(userRoles)); 
            return permissions;
        }
    }
}
