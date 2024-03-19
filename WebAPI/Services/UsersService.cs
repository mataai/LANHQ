using AutoMapper;
using Core.DTO.Users;
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
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<ApplicationUserDTO> GetUserById(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationUserDTO>(result);
        }

        public async Task<ApplicationUserDTO> GetUserByUsername(string username)
        {
            var result = await _userRepository.GetUserByUsername(username);
            return _mapper.Map<ApplicationUserDTO>(result);
        }

        public async Task<ApplicationUserDTO> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
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

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersWithPermission(Guid permissionId)
        {
            var users = await _userRepository.GetUsersWithPermission(permissionId);
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(users);
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersWithRole(Guid roleId)
        {
            var users = await _userRepository.GetUsersWithRole(roleId);
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(users);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(Guid userId)
        {
            return await _userRepository.GetRolesForUser(userId);
        }

        public async Task<IEnumerable<string>> GetRolesForUser(string username)
        {
            return await _userRepository.GetRolesForUser(username);
        }


        //TODO: Implement generic model for updates and creates
        public async Task<bool> AddUserToRole(Guid userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return await _userRepository.AddUserToRole(user, roleName);
        }

    }
}
