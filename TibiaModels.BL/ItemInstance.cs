namespace TibiaModels.BL
{
    public class ItemInstance
    {
        public ItemInstance()
        {

        }
        public int ItemInstanceId { get; set; }
        public int ItemId { get; set; }
        public Equipment Equipment { get; set; }
        public int EquipmentId { get; set; }
        public Item Item { get; set; }
        public int? ContainerId { get; set; }
        public int Quantity { get; set; }
    }
}
