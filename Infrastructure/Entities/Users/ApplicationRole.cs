using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities.Users
{
    public class ApplicationRole(string name, string description) : IdentityRole<Guid>(name)
    {
        public string Description { get; set; } = description;
        public ICollection<Permission> Permissions { get; set; } = [];
    }
}
