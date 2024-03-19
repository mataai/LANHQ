using Core.Attributes;
using Core.DTO.Permissions;
using Core.DTO.Users;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IPermissionFetchingService _permissionFetchingService;
        public UsersController(IUsersService usersService, IPermissionFetchingService permissionFetchingService)
        {
            _usersService = usersService;
            _permissionFetchingService = permissionFetchingService;
        }

        [HttpGet]
        [PermissionAuthorize("users.get")]
        public ActionResult<IEnumerable<ApplicationUserDTO>> Get() => Ok(_usersService.GetUsers());

        [HttpGet("{id}")]
        [PermissionAuthorize("users.getById")]
        [PermissionAuthorize("users.self")]
        public ActionResult<ApplicationUserDTO> Get(Guid id) => Ok(_usersService.GetUserById(id));

        [HttpGet("{id}/roles")]
        [PermissionAuthorize("users.getRoles")]
        public ActionResult<IEnumerable<string>> GetRoles(Guid id) => Ok(_usersService.GetRolesForUser(id));
         
        [HttpGet("{id}/permissions")]
        [PermissionAuthorize("users.getPermissions")]
        public async Task<ActionResult<IEnumerable<PermissionDTO>>> GetPermissions(Guid id)
        {
            var permissions = await _permissionFetchingService.GetPermissionsForUser(id);
            return Ok(permissions);
        }
    }
}
