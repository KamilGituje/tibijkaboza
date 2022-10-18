using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL;

namespace TibiaRepositories.BL.Interfaces
{
    public interface ICharacterService
    {
        bool IsValid(Character character);
        Task<Character> CreateAsync (Character character, Guid userId);
        Task<List<Item>> KillMonsterAsync (Character character, Monster monster);
        Task<List<Item>> GetLootAsync(Character character, List<Item> loot);
        List<Item> RandomLoot(List<ItemMonster> loot);
        Task<int> SellItemAsync(Npc npc, Item item, Character character);
        bool IsInBp(Character character, Item item);
        bool IsNpcBuying(Npc npc, Item item);
        Character SetLevel(Character character);
        List<Item> GetCharacterItemsInBp(Character character);
    }
}
