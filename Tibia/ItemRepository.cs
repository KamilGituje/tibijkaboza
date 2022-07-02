using System.Collections.Generic;
using System.Linq;

namespace Tibia
{
    public class ItemRepository
    {
        public ItemRepository()
        {
            ItemBase.Add(new Item()
            {
                Name = "Halberd",
                DropRate = 0.1,
                Weight = 60,
                Price = 400
            });
            ItemBase.Add(new Item()
            {
                Name = "Short sword",
                DropRate = 0.6,
                Weight = 35,
                Price = 20
            });
            ItemBase.Add(new Item()
            {
                Name = "Cyclops toe",
                DropRate = 0.3,
                Weight = 0.2,
                Price = 40
            });
            ItemBase.Add(new Item()
            {
                Name = "Katana",
                DropRate = 0.2,
                Weight = 30,
                Price = 15
            });
            ItemBase.Add(new Item()
            {
                Name = "Legion helmet",
                DropRate = 0.25,
                Weight = 40,
                Price = 15
            });
        }
        private List<Item> ItemBase = new List<Item>();

        public Item Get(string itemName)
        {
            return ItemBase.FirstOrDefault(item => item.Name == itemName);
        }
    }
}
