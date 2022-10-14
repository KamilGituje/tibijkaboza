using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface INpcRepository
    {
        Task<List<Npc>> GetNpcsAsync();
        Task<Npc> GetAsync(int npcId);
        Task<Npc> GetByNameAsync(string npcName);
        Task<Npc> GetWithItemsAsync(int npcId);
        Task<bool> IsExistAsync(string npcName);
        Task<Npc> GetWithItemsByNameAsync(string npcName);
    }
}
