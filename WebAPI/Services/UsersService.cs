using Infrastructure.DTO.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace WebAPI.Services
{
    public class UsersService
    {
        //create crud using the user repository
        private IUserRepository _userRepository;
        public UsersService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<ApplicationUserDTO> GetUserById(Guid id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<ApplicationUserDTO> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<ApplicationUserDTO> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }


        public async Task<ApplicationUserDTO> UpdateUser(ApplicationUserCreateDTO user)
        {
            return await _userRepository.UpdateUser(user);
        }

        public async Task<ApplicationUserDTO> DeactivateUser(Guid id)
        {
            return await _userRepository.DeactivateUser(id);
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersWithPermission(Guid permissionId)
        {
            return await _userRepository.GetUsersWithPermission(permissionId);
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId)
        {
            return await _userRepository.GetUsersWithRole(roleId);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            return await _userRepository.GetRolesForUser(userId);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(string username)
        {
            return await _userRepository.GetRolesForUser(username);
        }

    }
}
