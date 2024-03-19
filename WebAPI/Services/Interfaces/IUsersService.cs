using Core.DataContracts.Systems.Users;

namespace WebAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> DeactivateUser(Guid id);
        Task<IEnumerable<string>> GetRolesForUser(Guid userId);
        Task<IEnumerable<string>> GetRolesForUser(string username);
        Task<ApplicationUserDTO> GetUserByEmail(string email);
        Task<ApplicationUserDTO> GetUserById(Guid id);
        Task<ApplicationUserDTO> GetUserByUsername(string username);
        Task<IEnumerable<ApplicationUserDTO>> GetUsers();
        Task<IEnumerable<ApplicationUserDTO>> GetUsersWithPermission(Guid permissionId);
        Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId);
        Task<ApplicationUserDTO> UpdateUser(Guid userId, ApplicationUserUpdateDTO updateRequest);
        Task<bool> AddUserToRole(Guid userId, string roleName);
        Task<bool> RemoveUserFromRole(Guid userId, string roleName);

    }
}