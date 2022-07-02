using System.Collections.Generic;
using System.Linq;

namespace Tibia
{
    public class MonsterRepository
    {
        public MonsterRepository()
        {
            MobBase.Add(new Monster()
            {
                Name = "Dog",
                Exp = 0
            });

            MobBase.Add(new Monster()
            {
                Name = "Rotworm",
                Exp = 35,
                Loot =
                {
                    new ItemRepository().Get("Katana"),
                    new ItemRepository().Get("Legion helmet")
                }
            });

            MobBase.Add(new Monster()
            {
                Name = "Cyclops",
                Exp = 150,
                Loot =
                {
                    new ItemRepository().Get("Halberd"),
                    new ItemRepository().Get("Short sword"),
                    new ItemRepository().Get("Cyclops toe")
                }
            });
        }
        private static List<Monster> MobBase = new List<Monster>();
        public Monster Get(string monsterName)
        {
            var monster = MobBase.First(mob => mob.Name == monsterName);
            return monster;
        }
    }
}
