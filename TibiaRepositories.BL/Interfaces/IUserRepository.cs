using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(Guid userId);
        public Task<bool> SaveChangesAsync();
        public Task<bool> AddCharacterAsync(User user, Character character);
    }
}
