using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities.Users
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();

        public ApplicationRole(string name) : base(name)
        {
            
        }
    }
}
