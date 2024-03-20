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
        public async Task<ActionResult<IEnumerable<ApplicationUserDTO>>> Get() => Ok(await _usersService.GetUsers());


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
    }
}
