using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class EquipmentRepository
    {
        public Equipment Retrieve(string characterName)
        {
            var equipment = new Equipment();
            if (characterName == "Rosool")
            {
                equipment.Legs = "Golden legs";
                equipment.Helmet = "Demon Helmet";
                equipment.Boots = "Boots of haste";
                equipment.Armor = "Magic plate armor";
                equipment.Amulet = "Platinum amulet";
                equipment.Ring = "Crystal ring";
                equipment.Shield = "Mastermind shield";
                equipment.Weapon = "Magic sword";
            }
            else if(characterName == "Jacula")
            {
                equipment.Legs = "Demon legs";
                equipment.Helmet = "Zaoan Helmet";
                equipment.Boots = "Boots of haste";
                equipment.Armor = "Prismatic armor";
                equipment.Amulet = "Fox amulet";
                equipment.Ring = "Death ring";
                equipment.Shield = "Mastermind shield";
                equipment.Weapon = "Ratana";
            }
            else
            {
                Console.WriteLine("No such character");
            }
            return equipment;
        }
    }
}
