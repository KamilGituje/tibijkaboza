namespace TibiaModels.BL
{
    public class ItemNpc
    {
        public ItemNpc()
        {

        }
        public int NpcId { get; set; }
        public int ItemId { get; set; }
        public int Price { get; set; }
        public Item Item { get; set; }
        public Npc Npc { get; set; }
    }
}
