﻿namespace Core.DataContracts.Systems.Users
{
    public class ApplicationUserDTO
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string EmailConfirmed { get; set; }
    }
}
