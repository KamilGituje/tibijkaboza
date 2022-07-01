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
            var loot = GetLoot(monsterName);
            character.Equipment.Backpack.AddRange(loot);
            character.Experience = character.Experience + monster.Exp;
            Console.WriteLine($"You killed a {monsterName}");
            Console.WriteLine($"You Gained {monster.Exp} exp");
            foreach(var item in loot)
            {
                Console.WriteLine($"You looted a {item}");
            }
            characterRepository.Update(character);
        }
        public List<LootItem> Items { get; set; }
        public List<string> GetLoot(string monsterName)
        {
            var items = new MonsterRepository().Get(monsterName).Loot;
            var list = new List<string>();
            foreach (var item in items)
            {
                if (Utility.Drop(item.DropRate) == true)
                {
                    list.Add(item.ItemName);
                }
            }
            return list;
        }

    }
}
