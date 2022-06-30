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
        private EquipmentRepository equipmentRepository { get; set; }
        public Character Get(string charName)
        {
            var character = new Character();
            character = CharBase.First(charac => charac.CharacterName == charName);
            return character;
        }
        public static List<Character> CharBase = new List<Character>();
        public void Add(Character character)
        {
            CharBase.Add(character);
        }
        public void GetStats(string charName)
        {
            var character = new Character();
            character = new CharacterRepository().Get(charName);
            var exp = character.Experience;
            var lvl = character.Level;
            Console.WriteLine($"Level: {lvl}");
            Console.WriteLine($"Exp: {exp}");
        }
        public void UpdateStats(string charName, Monster monster)
        {
            var character = new Character();
            character = new CharacterRepository().Get(charName);
            character.Experience = character.Experience + monster.Exp;
            character.Level = SetLevel(character);
            character.Equipment.Backpack.AddRange(monster.Loot);
            CharBase.Insert(CharBase.FindIndex(index => index.CharacterName == charName), character);
        }
        public int SetLevel(Character character)
        {
            if(character.Experience >= 100 && character.Experience < 200)
            {
                character.Level = 2;
            }
            if (character.Experience >= 200 && character.Experience < 350)
            {
                character.Level = 3;
            }
            if (character.Experience >= 350 && character.Experience < 500)
            {
                character.Level = 4;
            }
            if (character.Experience >= 500 && character.Experience < 750)
            {
                character.Level = 5;
            }
            if (character.Experience >= 750 && character.Experience < 1000)
            {
                character.Level = 6;
            }
            return character.Level;
        }
    }
}
