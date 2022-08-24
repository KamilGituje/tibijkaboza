using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class ItemRepository : IItemRepository
    {
        public ItemRepository(PubContext _context)
        {
            context = _context;
        }
        private readonly PubContext context;

        public async Task<Item> GetAsync(int itemId)
        {
            return await context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
        }
        public async Task<Item> GetByNameAsync(string itemName)
        {
            return await context.Items.FirstOrDefaultAsync(i => i.Name == itemName);
        }
        public async Task<bool> IsExistAsync (string itemName)
        {
            return await context.Items.AnyAsync(i => i.Name == itemName);
        }
    }
}