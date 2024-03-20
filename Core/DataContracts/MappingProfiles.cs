using AutoMapper;
using Core.DataContracts.Systems.Permissions;
using Core.DataContracts.Systems.Users;

namespace Core.DataContracts
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Infrastructure.Entities.Users.ApplicationUser, ApplicationUserDTO>();
            CreateMap<ApplicationUserDTO, Infrastructure.Entities.Users.ApplicationUser>();

            CreateMap<Infrastructure.Entities.Users.ApplicationRole, ApplicationRoleDTO>();
            CreateMap<ApplicationRoleDTO, Infrastructure.Entities.Users.ApplicationRole>();

            CreateMap<Infrastructure.Entities.Users.Permission, PermissionDTO>();
            CreateMap<PermissionDTO, Infrastructure.Entities.Users.Permission>();


        }
    }
}
