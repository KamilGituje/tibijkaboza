using System.ComponentModel.DataAnnotations.Schema;

namespace TibiaModels.BL
{
    public class Equipment
    {
        public Equipment()
        {

        }
        public int EquipmentId { get; set; }
        public int HelmetInstanceId { get; set; }
        public int AmuletInstanceId { get; set; }
        public int ArmorInstanceId { get; set; }
        public int LegsInstanceId { get; set; }
        public int WeaponInstanceId { get; set; }
        public int ShieldInstanceId { get; set; }
        public int BootsInstanceId { get; set; }
        public int RingInstanceId { get; set; }
        public int BackpackInstanceId { get; set; }
        public Character Character { get; set; }
        public int CharacterId { get; set; }
        public List<ItemInstance> ItemInstances { get; set; }
    }
}
