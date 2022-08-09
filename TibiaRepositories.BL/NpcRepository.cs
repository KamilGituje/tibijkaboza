using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class NpcRepository : INpcRepository
    {
        public NpcRepository(PubContext _context)
        {
            context = _context;
        }
        private readonly PubContext context;
        public async Task<Npc> GetAsync (int npcId)
        {
            return await context.Npcs.FirstOrDefaultAsync(n => n.NpcId == npcId);
        }
        public async Task<Npc> GetByNameAsync (string npcName)
        {
            return await context.Npcs.FirstOrDefaultAsync(n => n.Name == npcName);
        }
        public async Task<Npc> GetWithItemsAsync (int npcId)
        {
            return await context.Npcs.Include(n => n.ItemNpcs).ThenInclude(itn => itn.Item).FirstOrDefaultAsync(n => n.NpcId == npcId);
        }
        public async Task<Npc> GetWithItemsByNameAsync(string npcName)
        {
            return await context.Npcs.Include(n => n.ItemNpcs).ThenInclude(itn => itn.Item).FirstOrDefaultAsync(n => n.Name == npcName);
        }
        public async Task<bool> IsExistAsync (string npcName)
        {
            return await context.Npcs.AnyAsync(n => n.Name == npcName);
        }
    }
}
