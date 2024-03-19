using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace Core.Services
{
    public class PermissionFetchingService(IPermissionsRepository permissionsRepo, IUserRepository userRepo) : IPermissionFetchingService
    {
        private readonly IPermissionsRepository _permissionsRepo = permissionsRepo;
        private readonly IUserRepository _userRepo = userRepo;

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
