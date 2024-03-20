﻿using AutoMapper;
using Core.DTO.Permissions;
using Core.DTO.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace WebAPI.Services
{
    public class RoleService
    {
        // create crud using the roles repository
        private IRolesRepository _roleRepository;
        private IMapper _mapper;
        public RoleService(IRolesRepository roleRepository, IMapper mapper)
        {
            this._roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationRoleDTO>> GetRoles() => _mapper.Map<IEnumerable<ApplicationRoleDTO>>(await _roleRepository.GetRoles());

        public async Task<ApplicationRoleDTO> GetRoleById(Guid id)
        {
            return _mapper.Map<ApplicationRoleDTO>(await _roleRepository.GetRole(id));
        }

        public async Task<ApplicationRoleDTO> GetRole(Guid id)
        {
            return _mapper.Map<ApplicationRoleDTO>(await _roleRepository.GetRole(id));
        }

        public async Task<ApplicationRoleDTO> CreateRole(ApplicationRoleCreateDTO role)
        {
            return _mapper.Map<ApplicationRoleDTO>(await _roleRepository.CreateRole(role.Name, role.Description));
        }

        public async Task<ApplicationRoleDTO> UpdateRole(Guid roleId, ApplicationRoleUpdateDTO role)
        {
            var roleEntity = await _roleRepository.GetRole(roleId);
            roleEntity.Name = role.Name;
            roleEntity.Description = role.Description;
            return _mapper.Map<ApplicationRoleDTO>(_roleRepository.UpdateRole(roleEntity));
        }

        public async Task<bool> DeleteRole(Guid roleName)
        {
            return await _roleRepository.DeleteRole(roleName);
        }

        public async Task<ApplicationRoleDTO> GetRoleByName(string roleName)
        {
            return _mapper.Map<ApplicationRoleDTO>(await _roleRepository.GetRoleByName(roleName));
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersInRole(string roleName)
        {
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(await _roleRepository.GetUsersInRole(roleName));
        }

        public async Task<IEnumerable<ApplicationUserDTO>> GetUsersNotInRole(string roleName)
        {
            return _mapper.Map<IEnumerable<ApplicationUserDTO>>(await _roleRepository.GetUsersNotInRole(roleName));
        }

        public Task<bool> RoleExists(string roleName)
        {
            return _roleRepository.RoleExists(roleName);
        }

        public Task<bool> UpdateRole(string roleName, string newRoleName)
        {
            return _roleRepository.UpdateRole(roleName, newRoleName);
        }
    }
}
