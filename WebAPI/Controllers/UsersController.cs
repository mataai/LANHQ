using Core.Attributes;
using Core.DTO.Users;
using Infrastructure.Entities.Users;
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
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [PermissionAuthorize("users.get")]
        public Task<IEnumerable<ApplicationUserDTO>> Get() => _usersService.GetUsers();

        [HttpGet("{id}")]
        [PermissionAuthorize("users.getById")]
        [PermissionAuthorize("users.self")]
        public Task<ApplicationUserDTO> Get(Guid id) => _usersService.GetUserById(id);

        [HttpPost]
        [PermissionAuthorize("users.assign_role")]
        public Task<bool> AddUserToRole([FromBody] AddUserToRoleDTO request) => _usersService.AddUserToRole(request.UserId, request.RoleName);
    }
}
