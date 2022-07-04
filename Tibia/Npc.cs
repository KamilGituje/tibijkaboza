using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class Npc
    {
        public Npc()
        {
            ItemsBuy = new List<NpcItem>();
        }
        public string Name { get; set; }
        public List<NpcItem> ItemsBuy { get; set; }
    }
}
