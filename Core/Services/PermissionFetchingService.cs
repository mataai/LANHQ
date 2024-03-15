using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace Core.Services
{
    public class PermissionFetchingService
    {
        private IPermissionsRepository _permissionsRepo;
        private IUserRepository _userRepo;
        public PermissionFetchingService(IPermissionsRepository permissionsRepo, IUserRepository userRepo)
        {
            _permissionsRepo = permissionsRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            var userRoles = await _userRepo.GetRolesForUser(userId);
            var permissions = new List<Permission>();
            permissions.AddRange(user.Permissions);
            permissions.AddRange(await _permissionsRepo.GetPermissionsForRoles(userRoles));
            return permissions;
        }
    }
}
