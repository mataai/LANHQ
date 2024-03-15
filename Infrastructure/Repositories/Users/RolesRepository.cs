﻿using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{

    public class RolesRepository : IRolesRepository
    {
        private readonly LANHQDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesRepository(LANHQDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationRole>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<ApplicationRole> GetRoleById(Guid roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                // TODO: throw custom exception
                throw new Exception("Role not found");
            }
            return role;
        }
        public async Task<ApplicationRole> GetRoleByName(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                // TODO: throw custom exception
                throw new Exception("Role not found");
            }
            return role;
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsersInRole(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsersNotInRole(string roleName)
        {
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return users.Except(usersInRole);
        }
        public async Task<bool> AddUserToRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);

            // todo no success

            return result.Succeeded;
        }
        public Task<bool> RemoveAllRolesFromUser(ApplicationUser user)
        {
            var roles = _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                // TODO error handling
                return Task.FromResult(false);
            }
            var result = _userManager.RemoveFromRolesAsync(user, roles.Result);
            return Task.FromResult(result.Result.Succeeded);

        }
        public async Task<bool> RemoveUserFromRole(ApplicationUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            // todo no success

            return result.Succeeded;
        }
        public async Task<bool> RemoveUsersFromRole(List<ApplicationUser> users, string roleName)
        {
            foreach (ApplicationUser user in users)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<bool> CreateRole(string roleName)
        {
            await _roleManager.CreateAsync(new ApplicationRole(roleName));
            return true;
        }
        public async Task<bool> UpdateRole(string roleName, string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                // TODO error handling
                return false;
            }
            role.Name = newRoleName;
            await _roleManager.UpdateAsync(role);
            return true;
        }
        public async Task<bool> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                // TODO error handling
                return false;
            }
            await _roleManager.DeleteAsync(role);
            return true;
        }
        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public Task<ApplicationRole> GetRole(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                // TODO: throw custom exception
                throw new Exception("Role not found");
            }
            return Task.FromResult(role);

        }
        public Task<ApplicationRole> CreateRole(ApplicationRole role)
        {
            var result = _context.Roles.Add(role);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);

        }
        public Task<ApplicationRole> UpdateRole(ApplicationRole role)
        {
            var result = _context.Roles.Update(role);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);
        }
        public bool DeleteRole(Guid id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
                return true;
            }
            else
            {
                // TODO: throw custom exception
                return false;
            }
        }

    }
}
