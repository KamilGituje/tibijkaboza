using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;

namespace TibiaRepositories.BL
{
    public class MonsterRepository
    {
        public MonsterRepository()
        {

        }
        public Monster Get(int monsterId)
        {
            using var context = new PubContext();
            var monster = context.Monsters.FirstOrDefault(m => m.MonsterId == monsterId);
            return monster;
        }
        public Monster GetWithItems(int monsterId)
        {
            using var context = new PubContext();
            var monster = context.Monsters.Include(m => m.ItemMonsters).ThenInclude(im => im.Item).FirstOrDefault(m => m.MonsterId == monsterId);
            return monster;
        }
    }
}
