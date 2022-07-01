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
                    new LootItem()
                    {
                        ItemName = "Katana",
                        DropRate = 0.2
                    },
                    new LootItem()
                    {
                        ItemName = "Legion helmet",
                        DropRate = 0.25
                    }
                }
            });

            MobBase.Add(new Monster()
            {
                Name = "Cyclops",
                Exp = 150,
                Loot =
                {
                    new LootItem()
                    {
                        ItemName = "Halberd",
                        DropRate = 0.1
                    },
                    new LootItem()
                    {
                        ItemName = "Short sword",
                        DropRate = 0.6
                    },
                    new LootItem()
                    {
                        ItemName = "Cyclops toe",
                        DropRate = 0.3
                    }
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
