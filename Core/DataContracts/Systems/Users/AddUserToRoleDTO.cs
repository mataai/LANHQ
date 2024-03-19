namespace Core.DataContracts.Systems.Users
{
    public class AddUserToRoleDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
