using Core.Attributes;
using Core.DataContracts.Systems.Permissions;
using Core.DataContracts.Systems.Users;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUsersService usersService, IPermissionFetchingService permissionFetchingService) : ControllerBase
    {
        private readonly IUsersService _usersService = usersService;
        private readonly IPermissionFetchingService _permissionFetchingService = permissionFetchingService;

        [HttpGet]
        //[PermissionAuthorize("users.get")]
        public async Task<ActionResult<IEnumerable<ApplicationUserDTO>>> Get([FromQuery] string? roles = null, [FromQuery] string? search = null)
        {

            var data = await _usersService.GetUsers();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [PermissionAuthorize("users.getById")]
        [PermissionAuthorize("users.self")]
        public async Task<ActionResult<ApplicationUserDTO>> Get(Guid id) => Ok(await _usersService.GetUserById(id));

        [HttpGet("{id}/roles")]
        [PermissionAuthorize("users.getRoles")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles(Guid id) => Ok(await _usersService.GetRolesForUser(id));

        [HttpGet("{id}/permissions")]
        [PermissionAuthorize("users.getPermissions")]
        public async Task<ActionResult<IEnumerable<PermissionDTO>>> GetPermissions(Guid id)
        {
            return Ok(await _permissionFetchingService.GetPermissionsForUser(id));
        }

        [HttpPost("{id}")]
        [PermissionAuthorize("users.update")]
        public async Task<ActionResult> Update(Guid id, [FromBody] ApplicationUserUpdateDTO user)
        {
            var result = await _usersService.UpdateUser(id, user);
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        [PermissionAuthorize("users.updateRoles")]
        public async Task<ActionResult> AddUserRole(Guid id, [FromBody] AddUserRolesDTO roles)
        {
            var result = await _usersService.AddUserRole(id, roles);
            return Ok(result);
        }

        [HttpDelete("{id}/roles")]
        [PermissionAuthorize("users.updateRoles")]
        public async Task<ActionResult> RemoveUserRole(Guid id, [FromBody] RemoveUserRolesDTO roles)
        {
            var result = await _usersService.RemoveUserRole(id, roles);
            return Ok(result);
        }

        [HttpPut("{id}/permissions")]
        [PermissionAuthorize("users.updatePermissions")]
        public async Task<ActionResult> AddUserPermission(Guid id, [FromBody] AddUserPermissionsDTO permissionsDTO)
        {
            var result = await _usersService.AddUserPermission(id, permissionsDTO);
            return Ok(result);
        }

        [HttpDelete("{id}/permissions")]
        [PermissionAuthorize("users.updatePermissions")]
        public async Task<ActionResult> RemoveUserPermission(Guid id, [FromBody] RemoveUserPermissionsDTO permissionsDTO)
        {
            var result = await _usersService.RemoveUserPermission(id, permissionsDTO);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [PermissionAuthorize("users.delete")]
        public async Task<ActionResult> DeactivateUser(Guid id)
        {
            var result = await _usersService.DeactivateUser(id);
            return Ok(result);
        }

    }
}
