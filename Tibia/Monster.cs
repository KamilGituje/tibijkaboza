using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class Monster
    {
        public Monster()
        {
            Loot = new List<LootItem>();
        }
        public string Name { get; set; }
        public List<LootItem> Loot { get; set; }
        public int Exp { get; set; }
    }
}
