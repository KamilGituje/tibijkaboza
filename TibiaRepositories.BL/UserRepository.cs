using DB1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(PubContext _context)
        {
            context = _context;
        }
        private readonly PubContext context;
        public async Task<User> GetAsync(Guid userId)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<bool> SaveChangesAsync()
        {
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddCharacterAsync(User user, Character character)
        {
            user.Characters.Add(character);
            await SaveChangesAsync();
            return true;
        }
    }
}
