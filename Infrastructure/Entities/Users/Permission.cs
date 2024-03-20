namespace Infrastructure.Entities.Users
{
        public class Permission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string System { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = [];
        public virtual ICollection<ApplicationRole> Roles { get; set; } = [];

        public Permission(string permission, string description)
        {
            System = permission.Split(".")[0];
            Action = permission.Split(".")[1];
            Description = description;
        }
        public Permission(string system, string action, string description)
        {
            System = system;
            Action = action;
            Description = description;
        }

        public override string ToString()
        {
            return $"{System}.{Action}";
        }

    }
}
