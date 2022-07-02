using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class Character
    {

        public Character ()
        {
            Equipment = new Equipment();
        }
        public string CharacterName { get; set; }
        public string Vocation { get; set; }
        public string Residence { get; set; }
        public string Guild { get; set; }
        public Equipment Equipment { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public double CurrentCapacity { get; set; }
        public int MaxCapacity { get; set; }
    }
}
