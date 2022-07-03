using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class LootItemRepository : ItemRepository
    {
        public LootItemRepository(string itemName)
        {
            var lootItem = new LootItem();
            var item = new ItemRepository().Get(itemName);
            lootItem.Name = item.Name;
            lootItem.Weight = item.Weight;
            LootItem = lootItem;
        }
        private LootItem LootItem = new LootItem();

        public LootItem SetDropRate(double dropRate)
        {
            LootItem.DropRate = dropRate;
            return LootItem;
        }
    }
}
