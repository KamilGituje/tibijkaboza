using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Character> GetAsync(int characterId);
        Task<Character> GetWithItemsAsync(int characterId);
        Task<bool> IsExistAsync(int characterId);
        Task<bool> SaveChangesAsync();
    }
}
