using AutoMapper;

namespace Core.DTO
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Infrastructure.Entities.Users.ApplicationUser, Core.DTO.Users.ApplicationUserDTO>();
            CreateMap<Core.DTO.Users.ApplicationUserDTO, Infrastructure.Entities.Users.ApplicationUser>();

            CreateMap<Infrastructure.Entities.Users.ApplicationRole, Core.DTO.Permissions.ApplicationRoleDTO>();
            CreateMap<Core.DTO.Permissions.ApplicationRoleDTO, Infrastructure.Entities.Users.ApplicationRole>();

            CreateMap<Infrastructure.Entities.Users.Permission, Core.DTO.Permissions.PermissionDTO>();
            CreateMap<Core.DTO.Permissions.PermissionDTO, Infrastructure.Entities.Users.Permission>();


        }
    }
}
