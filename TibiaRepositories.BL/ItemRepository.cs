using DB1;
using TibiaModels.BL;

namespace TibiaRepositories.BL
{
    public class ItemRepository
    {
        public ItemRepository()
        {

        }

        public Item Get(int itemId)
        {
            using var context = new PubContext();
            var item = context.Items.FirstOrDefault(i => i.ItemId == itemId);
            return item;
        }
        public Item GetByName(string itemName)
        {
            using var context = new PubContext();
            var item = context.Items.FirstOrDefault(i => i.Name == itemName);
            return item;
        }
        public bool IsExist (string itemName)
        {
            using var context = new PubContext();
            bool isExist = context.Items.Any(i => i.Name == itemName);
            return isExist;
        }
    }
}
