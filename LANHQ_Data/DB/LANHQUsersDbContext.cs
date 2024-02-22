using LANHQ_Data.Models.Users;
using Microsoft.AspNet.Identity.EntityFramework;


namespace LANHQ_Data.DB
{
    public class LANHQUsersDbContext : IdentityDbContext<User>
    {
        public LANHQUsersDbContext(string _connectionString)
        : base(_connectionString)
        {
        }
    }
}