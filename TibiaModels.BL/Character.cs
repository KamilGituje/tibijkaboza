using System.ComponentModel.DataAnnotations.Schema;


namespace TibiaModels.BL
{
    public class Character
    {

        public Character()
        {
            Equipment = new Equipment();
        }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Vocation { get; set; }
        public string Residence { get; set; }
        public string Guild { get; set; }
        public Equipment Equipment { get; set; }
        public int Lvl { get; set; }
        public int Experience { get; set; }
        public decimal CurrentCapacity { get; set; }
        public decimal MaxCapacity { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
