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
        public async Task<Character> GetWithItemsAsync(int characterId)
        {
            return await context.Characters.Include(c => c.Equipment).ThenInclude(e => e.ItemInstances).ThenInclude(ii => ii.Item).FirstOrDefaultAsync(c => c.CharacterId == characterId);
        }
        public async Task<bool> IsExistAsync(int characterId)
        {
            return await context.Characters.AnyAsync(c => c.CharacterId == characterId);
        }
        public async Task<bool> SaveChangesAsync()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}
