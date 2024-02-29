using Microsoft.EntityFrameworkCore;

using Infrastructure.Entities.Users;
using Infrastructure.Repositories.Users.Interfaces;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _context;

        public UserRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task ActivateAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeactivateAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddYears(100);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
