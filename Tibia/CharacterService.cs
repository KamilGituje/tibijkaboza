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
            npcRepository = new NpcRepository();
        }
        private CharacterRepository characterRepository { get; set; }
        private MonsterRepository monsterRepository { get; set; }
        private NpcRepository npcRepository { get; set; }

        
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
        public List<Item> GetLoot(string monsterName)
        {
            var items = monsterRepository.Get(monsterName).Loot;
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
        public void SellItem(string npcName, string itemName, string charName)
        {
            var character = characterRepository.Get(charName);
            var npc = npcRepository.GetNpc(npcName);
            var item = npc.ItemsBuy.First(item => item.Name == itemName);
            bool isNpcBuying = npc.ItemsBuy.Any(itemnpc => itemnpc.Name == itemName);
            bool isInBp = character.Equipment.Backpack.Any(itembp => itembp.Name == itemName);

            if (isNpcBuying)
            {
                if (isInBp)
                {
                    character.Equipment.Gold = character.Equipment.Gold + item.ItemPrice;
                    character.CurrentCapacity = character.CurrentCapacity + item.Weight;
                    character.Equipment.Backpack.Remove(character.Equipment.Backpack.First(item => item.Name == itemName));
                    characterRepository.Update(character);
                    Console.WriteLine($"You sold a {itemName} for {item.ItemPrice}");
                }
                else
                {
                    Console.WriteLine($"You don't have {itemName.ToLower()}");
                }
            }
            else
            {
                Console.WriteLine($"{npcName} doesn't buy {itemName.ToLower()}");
            }
        }
    }
}
