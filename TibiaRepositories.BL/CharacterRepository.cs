using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class CharacterRepository : ICharacterRepository
    {
        public CharacterRepository(PubContext _context)
        {
            context = _context;
        }
        private readonly PubContext context;
        public async Task<Character> GetAsync(int characterId)
        {
            return await context.Characters.FirstOrDefaultAsync(c => c.CharacterId == characterId);
        }
        public async Task<List<Character>> GetForUserAsync(Guid userId)
        {
            return await context.Characters.Where(c => c.UserId == userId).ToListAsync();
        }
        public async Task<Character> GetWithItemsAsync(int characterId)
        {
            return await context.Characters.Include(c => c.Equipment).ThenInclude(e => e.ItemInstances).ThenInclude(ii => ii.Item).FirstOrDefaultAsync(c => c.CharacterId == characterId);
        }
        public async Task<bool> IsExistAsync(int characterId)
        {
            return await context.Characters.AnyAsync(c => c.CharacterId == characterId);
        }
        public async Task<bool> AddAsync(Character character)
        {
            await context.Characters.AddAsync(character);
            await SaveChangesAsync();
            return true;
        }
        public async Task RemoveItemFromBackpackAsync(Character character, Item item)
        {
            context.ItemInstances.Remove(character.Equipment.ItemInstances.FirstOrDefault(ii => ii.ItemId == item.ItemId && ii.ContainerId == character.Equipment.BackpackInstanceId));
            await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Character>> GetCharactersPaged(int pageNum)
        {
            int pageSize = 10;
            return await context.Characters.OrderByDescending(c => c.Lvl).Skip((pageNum - 1) * pageSize).Take(pageSize + 1).ToListAsync();
        }
            
    }
}
