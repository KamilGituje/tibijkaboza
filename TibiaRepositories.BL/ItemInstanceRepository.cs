using DB1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class ItemInstanceRepository : IItemInstanceRepository
    {
        private readonly PubContext context;
        public ItemInstanceRepository(PubContext _context)
        {
            context = _context;
        }
        public async Task<int> AddAsync(ItemInstance itemInstance)
        {
            await context.ItemInstances.AddAsync(itemInstance);
            await context.SaveChangesAsync();
            return itemInstance.ItemInstanceId;
        }
        public async Task Remove(ItemInstance itemInstance)
        {
            context.ItemInstances.Remove(itemInstance);
            await context.SaveChangesAsync();
        }
    }
}
