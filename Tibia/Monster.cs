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
            Loot = new List<string>();
        }
        public string Name { get; set; }
        public List<string> Loot { get; set; }
        public int Exp { get; set; }
    }
}
