using Core.DTO.Users;

namespace WebAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<ApplicationUserDTO>> GetUsers();
        Task<ApplicationUserDTO> GetUserById(Guid id);
        Task<IEnumerable<string>> GetRolesForUser(Guid userId);
        Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId);
        Task<ApplicationUserDTO> UpdateUser(Guid userId, ApplicationUserUpdateDTO updateRequest);
        Task<bool> DeactivateUser(Guid id);
        Task<bool> AddUserRole(Guid userId, Guid roleId);
        Task<bool> RemoveUserRole(Guid userId, Guid roleId);
        Task<bool> AddUserPermission(Guid userId, Guid permissionId);
        Task<bool> RemoveUserPermission(Guid userId, Guid permissionID);

    }
}