using Core.DataContracts.Systems.Users;

namespace WebAPI.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetUsers();
        Task<ApplicationUserDTO> GetUserById(Guid id);
        Task<IEnumerable<string>> GetRolesForUser(Guid userId);
        Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId);
        Task<ApplicationUserDTO> UpdateUser(Guid userId, ApplicationUserUpdateDTO updateRequest);
        Task<bool> DeactivateUser(Guid id);
        Task<bool> AddUserRole(Guid userId, AddUserRolesDTO rolesDTO);
        Task<bool> RemoveUserRole(Guid userId, RemoveUserRolesDTO rolesDTO);
        Task<bool> AddUserPermission(Guid userId, AddUserPermissionsDTO permissionsDTO);
        Task<bool> RemoveUserPermission(Guid userId, RemoveUserPermissionsDTO permissionsDTO);

    }
}