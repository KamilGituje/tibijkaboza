using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class LootItemRepository
    {
        public LootItemRepository(string monsterName)
        {
            Items = new List<LootItem>();
            if (monsterName == "Rotworm")
            {
                Items.Add(new LootItem()
                {
                    ItemName = "Katana",
                    DropRate = 0.2
                });
                Items.Add(new LootItem()
                {
                    ItemName = "Legion Helmet",
                    DropRate = 0.25
                });
            }
            if (monsterName == "Cyclops")
            {
                Items.Add(new LootItem()
                {
                    ItemName = "Short sword",
                    DropRate = 0.6
                });
                Items.Add(new LootItem()
                {
                    ItemName = "Halberd",
                    DropRate = 0.1
                });
                Items.Add(new LootItem()
                {
                    ItemName = "Cyclops toe",
                    DropRate = 0.3
                });
            }
        }
        public List<LootItem> Items { get; set; }
        public List<string> GetLoot()
        {
            var items = Items;
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
