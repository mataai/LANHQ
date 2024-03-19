using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities.Users
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();

        public ApplicationRole(string name, string description) : base(name)
        {
            Description = description;
        }
    }
}
