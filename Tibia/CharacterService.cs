using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class CharacterService
    {
        public CharacterService()
        {
            characterRepository = new CharacterRepository();
            monsterRepository = new MonsterRepository();
        }
        private CharacterRepository characterRepository { get; set; }
        private MonsterRepository monsterRepository { get; set; }
        
        public void KillMonster (string charName, string monsterName)
        {
            var character = characterRepository.Get(charName);
            var monster = monsterRepository.Get(monsterName);
            var loot = new LootItemRepository(monsterName).GetLoot();
            character.Equipment.Backpack.AddRange(loot);
            character.Experience = character.Experience + monster.Exp;
            Console.WriteLine($"You killed a {monsterName}");
            Console.WriteLine($"You Gained {monster.Exp} exp");
            foreach(var item in loot)
            {
                Console.WriteLine($"You looted a {item}");
            }
        }

    }
}
