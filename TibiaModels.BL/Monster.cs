using System.ComponentModel.DataAnnotations.Schema;

namespace TibiaModels.BL
{
    public class Monster
    {
        public Monster()
        {

        }
        public int MonsterId { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public List<ItemMonster> ItemMonsters { get; set; }
    }
}
