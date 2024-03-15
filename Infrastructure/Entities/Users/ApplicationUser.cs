using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Permission> Permissions { get; set; }

        public ApplicationUser()
        {
            Permissions = new List<Permission>();
        }
    }
}
