using AutoMapper;
using Core.DataContracts.Systems.Users;
using Core.Services;
using Infrastructure.Repositories.Users;

namespace WebAPI.Services.Internal
{
    public class UsersService(IUserRepository userRepository, IMapper mapper, IPermissionFetchingService permissionFetchingService) : IUsersService
    {
        //create crud using the user repository
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPermissionFetchingService _permissionFetchingService = permissionFetchingService;

        public async Task<ApplicationUserDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var roles = await _userRepository.GetUserRoles(id);
            var userDto = _mapper.Map<ApplicationUserDTO>(user);
            //userDto.Roles = roles;
            userDto.Permissions = await _permissionFetchingService.GetPermissionsForUser(id);

            return userDto;
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
            var mapped = _mapper.Map<IEnumerable<ApplicationUserDTO>>(users);
            return mapped;
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
        public async Task<bool> AddUserRole(Guid userId, AddUserRolesDTO rolesDTO)
        {
            return await _userRepository.AddUserRoles(userId, rolesDTO);
        }

        public Task<bool> RemoveUserRole(Guid userId, RemoveUserRolesDTO roleId)
        {
            return _userRepository.RemoveUserRole(userId, roleId.ToString());
        }

        public Task<bool> AddUserPermission(Guid userId, AddUserPermissionsDTO permissionId)
        {
            throw new NotImplementedException();    
        }

        public Task<bool> RemoveUserPermission(Guid userId, RemoveUserPermissionsDTO permissionID)
        {
            throw new NotImplementedException();
        }

    }
}
