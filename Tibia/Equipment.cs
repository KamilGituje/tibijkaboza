using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class Equipment
    {
        public Equipment()
        {

        }
        public string Helmet { get; set; }
        public string Amulet { get; set; }
        public string Armor { get; set; }
        public string Legs { get; set; }
        public string Weapon { get; set; }
        public string Shield { get; set; }
        public string Boots { get; set; }
        public string Ring { get; set; }
        public List<string> Backpack = new List<string>();
    }
}
