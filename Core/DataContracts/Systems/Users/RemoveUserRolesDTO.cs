namespace Core.DataContracts.Systems.Users
{
    public class RemoveUserRolesDTO
    {
        public required IEnumerable<Guid> RoleIds { get; set; }
    }
}
