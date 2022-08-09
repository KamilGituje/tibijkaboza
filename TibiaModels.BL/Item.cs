using System.ComponentModel.DataAnnotations.Schema;

namespace TibiaModels.BL
{
    public class Item
    {
        public Item()
        {

        }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public List<ItemInstance> ItemInstances { get; set; }
        public List<ItemMonster> ItemMonster { get; set; }
        public List<ItemNpc> ItemNpcs { get; set; }
        [NotMapped]
        public int? Quantity { get; set; }
    }
}
