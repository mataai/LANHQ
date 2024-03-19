using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace Infrastructure.Repositories.Users
{
    public class PermissionsRepository(LANHQDbContext Context) : IPermissionsRepository
    {

        public async Task<Permission> GetPermission(Guid id)
        {
            var permission = await Context.Permissions.FindAsync(id) ?? throw new Exception("Permission not found");
            return permission;
        }

        public Task<IEnumerable<Permission>> GetPermissions()
        {
            return Task.FromResult(Context.Permissions.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissions(string system)
        {
            var data = Context.Permissions.Where(p => p.System == system);
            return Task.FromResult(data.AsEnumerable());
        }

        public Task<IEnumerable<Permission>> GetPermissionsForRoles(IEnumerable<string> roleNames)
        {
            var data = Context.Roles.Where(r => roleNames.Contains(r.Name)).SelectMany(r => r.Permissions);
            return Task.FromResult(data.AsEnumerable());
        }

        public Task<IEnumerable<ApplicationRole>> GetRolesWithPermission(Guid permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUsersWithPermission(Guid permissionId)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Permission> CreatePermission(Permission permission)
        {
            var result = await Context.Permissions.AddAsync(permission);
            Context.SaveChanges();
            return result.Entity;
        }

        public Task<Permission> UpdatePermission(Permission permission)
        {
            var result = Context.Permissions.Update(permission);
            Context.SaveChanges();
            return Task.FromResult(result.Entity);
        }

        public bool DeletePermission(Guid id)
        {
            var permission = Context.Permissions.Find(id);
            if (permission != null)
            {
                Context.Permissions.Remove(permission);
                return Context.SaveChanges() >= 1;
            }
            else
            {
                // TODO: throw custom exception
                throw new Exception("404 Permission not found");
            }
        }
    }
}
