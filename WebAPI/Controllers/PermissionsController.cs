using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /**
     * Roles and Claims Controller
     */
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        public PermissionsController(RolesService)
        {

        }
    }
}
