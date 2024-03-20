using AutoMapper;
using Core.DTO.Users;
using Infrastructure.Repositories.Users;
using Infrastructure.Repositories.Users.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class UsersService : IUsersService
    {
        //create crud using the user repository
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public UsersService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDTO> GetUserById(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationUserDTO>(result);
        }

        public async Task<ApplicationUserDTO> UpdateUser(Guid userId, ApplicationUserUpdateDTO updateRequest)
        {
            /* TODO check permissions of the executing user for specific fields
             * ex: 
             * ADMIN => update all fields
             * SELF => update only self username address and etc...
             */
            var user = await _userRepository.GetByIdAsync(userId);

            user.UserName = updateRequest.UserName;

            var result = await _userRepository.UpdateAsync(user);
            return _mapper.Map<ApplicationUserDTO>(result);
        }

        //TODO: Implement generic model for updates and creates
        public Task<bool> DeactivateUser(Guid id) => _userRepository.DeactivateAsync(id);

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(users);
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId)
        {
            var users = await _userRepository.GetUsersWithRole(roleId);
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(users);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            return await _userRepository.GetUserRoles(userId);
        }

        //TODO: Implement generic model for updates and creates
        public async Task<bool> AddUserRole(Guid userId, Guid roleId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return await _userRepository.AddUserToRole(user, roleId);
        }

        public Task<bool> RemoveUserRole(Guid userId, Guid roleId)
        {
            return _userRepository.RemoveUserFromRole(userId, roleId);
        }

        public Task<bool> AddUserPermission(Guid userId, Guid permissionId)
        {


        }

        public Task<bool> RemoveUserPermission(Guid userId, Guid permissionID)
        {
            throw new NotImplementedException();
        }
    }
}
