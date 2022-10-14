using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class MonsterRepository : IMonsterRepository
    {
        public MonsterRepository(PubContext _context)
        {
            context = _context;
        }
        private readonly PubContext context;
        public async Task<Monster> GetAsync(int monsterId)
        {
            return await context.Monsters.FirstOrDefaultAsync(m => m.MonsterId == monsterId);
        }
        public async Task<Monster> GetWithItemsAsync(int monsterId)
        {
            return await context.Monsters.Include(m => m.ItemMonsters).ThenInclude(im => im.Item).FirstOrDefaultAsync(m => m.MonsterId == monsterId);
        }
        public async Task<List<Monster>> GetMonstersAsync()
        {
            return await context.Monsters.ToListAsync();
        }
    }
}
