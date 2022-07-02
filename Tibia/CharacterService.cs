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
            itemRepository = new ItemRepository();
        }
        private CharacterRepository characterRepository { get; set; }
        private MonsterRepository monsterRepository { get; set; }
        private ItemRepository itemRepository { get; set; }
        
        public void KillMonster (string charName, string monsterName)
        {
            var character = characterRepository.Get(charName);
            var monster = monsterRepository.Get(monsterName);
            character.Experience = character.Experience + monster.Exp;
            Console.WriteLine($"You killed a {monsterName}");
            Console.WriteLine($"You Gained {monster.Exp} exp");
            var loot = GetLoot(monsterName);
            foreach (var item in loot)
            {
                if (item.Weight <= character.CurrentCapacity)
                {
                    character.Equipment.Backpack.Add(item);
                    character.CurrentCapacity = character.CurrentCapacity - item.Weight;
                    Console.WriteLine($"You looted a {item.Name}");
                }
                else
                {
                    Console.WriteLine($"{item.Name} is too heavy to carry for you");
                }
            }
            characterRepository.Update(character);
        }
        public List<Item> Items { get; set; }
        public List<Item> GetLoot(string monsterName)
        {
            var items = new MonsterRepository().Get(monsterName).Loot;
            var list = new List<Item>();
            foreach (var item in items)
            {
                if (Utility.Drop(item.DropRate) == true)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public void SellLoot (string itemName, string charName)
        {
            var character = characterRepository.Get(charName);
            character.Equipment.Gold = character.Equipment.Gold + itemRepository.Get(itemName).Price;
            character.CurrentCapacity = character.CurrentCapacity + itemRepository.Get(itemName).Weight;
            characterRepository.Update(character);
        }
    }
}
