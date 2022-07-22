using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;

namespace TibiaRepositories.BL
{
    public class CharacterRepository
    {
        public CharacterRepository()
        {

        }
        public Character Get(int characterId)
        {
            using var context = new PubContext();
            var character = context.Characters.FirstOrDefault(c => c.CharacterId == characterId);
            return character;
        }
        public Character GetWithItems(int characterId)
        {
            using var context = new PubContext();
            var character = context.Characters.Include(c => c.Equipment).ThenInclude(e => e.ItemInstances).ThenInclude(ii => ii.Item).FirstOrDefault(c => c.CharacterId == characterId);
            return character;
        }
        public bool IsExist(int characterId)
        {
            using var context = new PubContext();
            var isExist = context.Characters.Any(c => c.CharacterId == characterId);
            return isExist;
        }
    }
}
