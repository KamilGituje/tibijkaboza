using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class EquipmentRepository
    {
        public EquipmentRepository()
        {

        }

        public Equipment Get(string charName)
        {
            var equipment = new Equipment();
            equipment = CharacterRepository.CharBase.First(eq => eq.CharacterName == charName).Equipment;

            return equipment;
        }
    }
}
