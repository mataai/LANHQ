using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;
using System.Linq;

namespace Infrastructure.Repositories.Users
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly LANHQDbContext _context;
        public PermissionsRepository(LANHQDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;

        }

        public async Task<Permission> CreatePermission(Permission permission)
        {
            var result = await _context.Permissions.AddAsync(permission);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool DeletePermission(Guid id)
        {
            var permission = _context.Permissions.Find(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                _context.SaveChanges();
                return true;
            }
            else
            {
                // TODO: throw custom exception
            }
            return false;
        }

        public async Task<Permission> GetPermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                // TODO: throw custom exception
                throw new Exception("Permission not found");
            }
            return permission;
        }

        public Task<IEnumerable<Permission>> GetPermissions()
        {
            return Task.FromResult(_context.Permissions.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissions(string system)
        {
            var data = _context.Permissions.Where(p => p.System == system);
            return Task.FromResult(data.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<Guid> roleIds)
        {
            var data = _context.Roles.Where(r => roleIds.Contains(r.Id)).SelectMany(r => r.Permissions);
            return Task.FromResult(data.AsEnumerable());
        }
        public Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<string> roleNames)
        {
            var data = _context.Roles.Where(r => roleNames.Contains(r.Name)).SelectMany(r => r.Permissions);
            return Task.FromResult(data.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissionsForRole(Guid roleId)
        {
            var role = _context.Roles.Find(roleId);
            if (role == null)
            { 
                // TODO: throw custom exception
                return null;
            }
            return Task.FromResult(role.Permissions.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissionsForRole(string roleId)
        {
            var role = _context.Roles.Find(roleId);
            if (role == null)
            {
                // TODO: throw custom exceptionk
                return null;
            }

            return Task.FromResult(role.Permissions.AsEnumerable());
        }

        public async Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user.Permissions.AsEnumerable();
        }

        public Task<IEnumerable<ApplicationRole>> GetRolesWithPermission(Guid permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<Permission> UpdatePermission(Permission permission)
        {
            var result = _context.Permissions.Update(permission);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);
        }
    }
}
