using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class MonsterRepository
    {
        public MonsterRepository()
        {
            characterRepository = new CharacterRepository();
        }
        private CharacterRepository characterRepository { get; set; }
        public void GetDog()
        {
            var monster = new Monster();
            monster.MonsterName = "Dog";
            monster.Exp = 0;
            Console.WriteLine($"You killed a {monster.MonsterName}");
        }
        public void GetRotworm(string charName)
        {
            var monster = new Monster();
            monster.MonsterName = "Rotworm";
            monster.Exp = 35;
            if (Utility.Drop(0.2) == true)
            {
                monster.Loot.Add("Katana");
            }
            if (Utility.Drop(0.25) == true)
            {
                monster.Loot.Add("Legion helmet");
            }
            Console.WriteLine($"You killed a {monster.MonsterName}");
            Console.WriteLine("You gained " + monster.Exp + " exp");
            foreach (var item in monster.Loot)
            {
                Console.WriteLine("You looted " + item);
            }
            characterRepository.UpdateStats(charName, monster);
        }
        public void GetCyclops(string charName)
        {
            var monster = new Monster();
            monster.MonsterName = "Cyclops";
            monster.Exp = 150;
            if (Utility.Drop(0.6) == true)
            {
                monster.Loot.Add("Short sword");
            }
            if (Utility.Drop(0.1) == true)
            {
                monster.Loot.Add("Halberd");
            }
            if (Utility.Drop(0.3) == true)
            {
                monster.Loot.Add("Cyclops toe");
            }
            Console.WriteLine($"You killed a {monster.MonsterName}");
            Console.WriteLine("You gained " + monster.Exp + " exp");
            foreach (var item in monster.Loot)
            {
                Console.WriteLine("You looted " + item);
            }
            characterRepository.UpdateStats(charName, monster);
        }
    }
}
