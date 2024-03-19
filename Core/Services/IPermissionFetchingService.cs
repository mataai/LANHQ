using Infrastructure.Entities.Users;

namespace Core.Services
{
    public interface IPermissionFetchingService
    {
        Task<IEnumerable<Permission>> GetPermissionsForUser(Guid userId);
    }
}