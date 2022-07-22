using System.ComponentModel.DataAnnotations.Schema;

namespace TibiaModels.BL
{
    public class Npc
    {
        public Npc()
        {
            
        }
        public int NpcId { get; set; }
        public string Name { get; set; }
        public List<ItemNpc> ItemNpcs { get; set; }
    }
}
