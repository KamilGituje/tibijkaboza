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
        Task<List<Character>> GetForUserAsync(Guid userId);
        Task<Character> GetWithItemsAsync(int characterId);
        Task<bool> IsExistAsync(int characterId);
        Task<bool> AddAsync(Character character);
        Task RemoveItemFromBackpackAsync(Character character, Item item);
        Task<bool> SaveChangesAsync();
        Task<List<Character>> GetCharactersPaged(int pageNum);
    }
}
