namespace Core.DataContracts.Systems.Users
{
    public class AddUserPermissionsDTO
    {
        public required IEnumerable<Guid> RoleIds { get; set; }
    }
}
