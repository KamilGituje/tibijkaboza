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
                    new LootItemRepository("Katana").SetDropRate(0.2),
                    new LootItemRepository("Legion helmet").SetDropRate(0.25)
                }
            });

            MobBase.Add(new Monster()
            {
                Name = "Cyclops",
                Exp = 150,
                Loot =
                {
                    new LootItemRepository("Halberd").SetDropRate(0.1),
                    new LootItemRepository("Short sword").SetDropRate(0.6),
                    new LootItemRepository("Cyclops toe").SetDropRate(0.3)
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
