using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class NpcItemRepository : ItemRepository
    {
        public NpcItemRepository(string itemName)
        {
            var npcItem = new NpcItem();
            var item = new ItemRepository().Get(itemName);
            npcItem.Name = item.Name;
            npcItem.Weight = item.Weight;
            NpcItem = npcItem;
        }
        private NpcItem NpcItem = new NpcItem();
        public NpcItem SetPrice (int price)
        {
            NpcItem.ItemPrice = price;
            return NpcItem;
        }

    }
}
