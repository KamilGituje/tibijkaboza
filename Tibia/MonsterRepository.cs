using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Loot = new LootItemRepository("Rotworm").GetLoot()
            });

            MobBase.Add(new Monster()
            {
                Name = "Cyclops",
                Exp = 150,
                Loot = new LootItemRepository("Cyclops").GetLoot()
            });
        }
        public static List<Monster> MobBase = new List<Monster>();
        public Monster Get (string monsterName)
        {
            var monster = MobBase.First(mob => mob.Name == monsterName);
            return monster;
        }
    }
}
