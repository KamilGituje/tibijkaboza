using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;

namespace TibiaRepositories.BL
{
    public class NpcRepository
    {
        public NpcRepository()
        {

        }
        public Npc Get (int npcId)
        {
            using var context = new PubContext();
            var npc = context.Npcs.FirstOrDefault(n => n.NpcId == npcId);
            return npc;
        }
        public Npc GetByName (string npcName)
        {
            using var context = new PubContext();
            var npc = context.Npcs.FirstOrDefault(n => n.Name == npcName);
            return npc;
        }
        public Npc GetWithItems (int npcId)
        {
            using var context = new PubContext();
            var npc = context.Npcs.Include(n => n.ItemNpcs).ThenInclude(itn => itn.Item).FirstOrDefault(n => n.NpcId == npcId);
            return npc;
        }
        public bool IsExist (string npcName)
        {
            using var context = new PubContext();
            bool isExist = context.Npcs.Any(n => n.Name == npcName);
            return isExist;
        }
    }
}
