namespace TibiaAPI.Models
{
    public class CharacterWithoutEquipmentDto
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Vocation { get; set; }
        public string Residence { get; set; }
        public string Guild { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
        public decimal CurrentCapacity { get; set; }
        public decimal MaxCapacity { get; set; }
    }
}