using Infrastructure.DTO.Permissions;
using Infrastructure.DTO.Users;
using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace WebAPI.Services
{
    public class RoleService
    {
        // create crud using the roles repository
        private IRolesRepository _roleRepository;
        public RoleService(IRolesRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            return await _roleRepository.(userId);
        }
        
        public async Task<IEnumerable<ApplicationRoleDTO>> GetRoles()
        {
            return await _roleRepository.GetRoles();
        }

        public async Task<ApplicationRoleDTO> GetRoleById(Guid id)
        {
            return await _roleRepository.GetRole(id);
        }

        public Task<IEnumerable<string>> GetRolesForUserName(string username)
        {
            return _roleRepository.GetRolesForUserName(username);
        }

        public Task<IEnumerable<string>> GetRolesForUserEmail(string username)
        {
            return _roleRepository.GetRolesForUserEmail(username);
        }

        public Task<ApplicationRoleDTO> GetRole(Guid id)
        {
            return _roleRepository.GetRole(id);
        }

        public Task<ApplicationRoleDTO> CreateRole(ApplicationRoleCreateDTO role)
        {
            return _roleRepository.CreateRole(role);
        }

        public Task<ApplicationRoleDTO> UpdateRole(ApplicationRoleUpdateDTO role)
        {
            return _roleRepository.UpdateRole(role);
        }

        public Task<bool> AddUserToRole(int userId, string roleName)
        {
            return _roleRepository.AddUserToRole(userId, roleName);
        }

        public Task<bool> CreateRole(string roleName)
        {
            return _roleRepository.CreateRole(roleName);
        }

        public Task<bool> DeleteRole(string roleName)
        {
            return _roleRepository.DeleteRole(roleName);
        }

        public Task<ApplicationRoleDTO> GetRoleByName(string roleName)
        {
            return _roleRepository.GetRoleByName(roleName);
        }

        public Task<IEnumerable<ApplicationUserDTO>> GetUsersInRole(string roleName)
        {
            return _roleRepository.GetUsersInRole(roleName);
        }

        public Task<IEnumerable<ApplicationUserDTO>> GetUsersNotInRole(string roleName)
        {
            return _roleRepository.GetUsersNotInRole(roleName);
        }

        public Task<bool> RemoveUserFromRole(int userId, string roleName)
        {
            return _roleRepository.RemoveUserFromRole(userId, roleName);
        }
        
        public Task<bool> RemoveUsersFromRole(int userId, string roleName)
        {
            return _roleRepository.RemoveUserFromRole(userId, roleName);
        }

        public Task<bool> RoleExists(string roleName)
        {
            return _roleRepository.RoleExists(roleName);
        }

        public Task<bool> UpdateRole(string roleName, string newRoleName)
        {
            return _roleRepository.UpdateRole(roleName, newRoleName);
        }
    }
}
