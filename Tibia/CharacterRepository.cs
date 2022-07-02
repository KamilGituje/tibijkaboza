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

        }
        public Character Get(string charName)
        {
            var character = CharBase.FirstOrDefault(charac => charac.CharacterName == charName);
            return character;
        }
        private static List<Character> CharBase = new List<Character>();
        public void Add(Character character)
        {
            CharBase.Add(character);
        }
        public void GetStats(string charName)
        {
            var character = Get(charName);
            Console.WriteLine($"Level: {character.Level}");
            Console.WriteLine($"Exp: {character.Experience}");
        }
        public void Update(Character character)
        {
            character.Level = GetLevel(character.Experience);
            if(character.CurrentCapacity > character.MaxCapacity)
            {
                character.CurrentCapacity = character.MaxCapacity;
            }
            CharBase.Insert(CharBase.FindIndex(index => index.CharacterName == character.CharacterName), character);
        }
        public int GetLevel(int exp)
        {
            int level = 1;
            if(exp >= 100 && exp < 200)
            {
                level = 2;
            }
            if (exp >= 200 && exp < 350)
            {
                level = 3;
            }
            if (exp >= 350 && exp < 500)
            {
                level = 4;
            }
            if (exp >= 500 && exp < 750)
            {
                level = 5;
            }
            if (exp >= 750)
            {
                level = 6;
            }
            return level;
        }
    }
}
