using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class CharacterRepository
    {
        public CharacterRepository()
        {
            equipmentRepository = new EquipmentRepository();
        }
        private EquipmentRepository equipmentRepository = new EquipmentRepository();
        public Character Retrieve(string characterName)
        {
            var character = new Character(characterName);
            character.CharacterName = characterName;
            character.Equipment = equipmentRepository.Retrieve(characterName);
            if(characterName == "Jacula")
            {
                character.Level = 100;
                character.Guild = "Onwer of Thais";
                character.Vocation = "Knight";
            }
            else if(characterName == "Rosool")
            {
                character.Level = 87;
                character.Guild = "We dont care";
                character.Vocation = "Elite Knight";
            }
            return character;
        }
    }
}
