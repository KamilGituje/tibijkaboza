using System.ComponentModel.DataAnnotations.Schema;

namespace TibiaModels.BL
{
    public class ItemMonster
    {
        public ItemMonster()
        {

        }
        public int MonsterId { get; set; }
        public int ItemId { get; set; }
        public Monster Monster { get; set; }
        public Item Item { get; set; }
        public decimal DropRate { get; set; }
    }
}
