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

        }
        public Character (string characterName)
        {
            CharacterName = characterName;
            Equipment = new Equipment();
        }
        public string CharacterName { get; set; }
        public string Vocation { get; set; }
        public int Level { get; set; }
        public string Guild { get; set; }
        public Equipment Equipment { get; set; }
    }
}
