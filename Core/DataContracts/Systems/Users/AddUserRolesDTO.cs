namespace Core.DataContracts.Systems.Users
{
    public class AddUserRolesDTO
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
