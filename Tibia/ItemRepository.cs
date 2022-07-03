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
                Weight = 60
            });
            ItemBase.Add(new Item()
            {
                Name = "Short sword",
                Weight = 35
            });
            ItemBase.Add(new Item()
            {
                Name = "Cyclops toe",
                Weight = 0.2
            });
            ItemBase.Add(new Item()
            {
                Name = "Katana",
                Weight = 30
            });
            ItemBase.Add(new Item()
            {
                Name = "Legion helmet",
                Weight = 40
            });
        }
        private List<Item> ItemBase = new List<Item>();

        public Item Get(string itemName)
        {
            return ItemBase.FirstOrDefault(item => item.Name == itemName);
        }
    }
}
