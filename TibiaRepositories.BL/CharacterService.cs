using DB1;
using TibiaModels.BL;

namespace TibiaRepositories.BL
{
    public class CharacterService
    {
        public CharacterService()
        {
            characterRepository = new CharacterRepository();
            monsterRepository = new MonsterRepository();
            npcRepository = new NpcRepository();
            itemRepository = new ItemRepository();
        }
        private CharacterRepository characterRepository { get; set; }
        private MonsterRepository monsterRepository { get; set; }
        private NpcRepository npcRepository { get; set; }
        private ItemRepository itemRepository { get; set; }

        public bool isValid(Character character)
        {
            bool isValid = false;
            if (!string.IsNullOrWhiteSpace(character.Name))
                if (!string.IsNullOrWhiteSpace(character.Vocation))
                    if (!string.IsNullOrWhiteSpace(character.Residence))
                    {
                        isValid = true;
                    }
            return isValid;
        }
        public void Create(Character character)
        {
            using var context = new PubContext();
            context.Characters.Add(character);
            context.SaveChanges();
            var backpackId = itemRepository.GetByName("Jacula backpack").ItemId;
            var backpack = new ItemInstance
            {
                ItemId = backpackId,
                EquipmentId = character.Equipment.EquipmentId,
            };
            context.ItemInstances.Add(backpack);
            context.SaveChanges();
            character.Equipment.BackpackInstanceId = backpack.ItemInstanceId;
            context.Update(character);
            context.SaveChanges();
        }
        public List<Item> KillMonster(Character character, Monster monster)
        {
            using var context = new PubContext();
            character.Experience = character.Experience + monster.Experience;
            var loot = RandomLoot(monster.ItemMonsters);
            character = SetLevel(character);
            context.Update(character);
            context.SaveChanges();
            return loot;
        }
        public List<Item> GetLoot(Character character, List<Item> loot)
        {
            using var context = new PubContext();
            var itemsCarried = new List<Item>();
            foreach (var item in loot)
            {
                if (item.Weight <= character.CurrentCapacity)
                {
                    var itemInstance = new ItemInstance()
                    {
                        ItemId = item.ItemId,
                        EquipmentId = character.Equipment.EquipmentId,
                        ContainerId = character.Equipment.BackpackInstanceId
                    };
                    context.ItemInstances.Add(itemInstance);
                    character.CurrentCapacity = character.CurrentCapacity - item.Weight;
                    itemsCarried.Add(item);
                }
            }
            context.Update(character);
            context.SaveChanges();
            return itemsCarried;
        }
        public List<Item> RandomLoot(List<ItemMonster> loot)
        {
            var list = new List<Item>();
            foreach (var item in loot)
            {
                if (Utility.Drop(item.DropRate) == true)
                {
                    list.Add(item.Item);
                }
            }
            return list;
        }
        public int SellItem(int npcId, int itemId, Character character)
        {
            using var context = new PubContext();
            var npc = npcRepository.GetWithItems(npcId);
            var item = npc.ItemNpcs.FirstOrDefault(itn => itn.ItemId == itemId);
            var goldId = itemRepository.GetByName("Gold coin").ItemId;
            bool hasCharMoney = character.Equipment.ItemInstances.Any(ii => ii.ItemId == goldId && ii.ContainerId == character.Equipment.BackpackInstanceId);
            var coinWeight = itemRepository.Get(goldId).Weight;
            var quantity = item.Price;

            if (!hasCharMoney)
            {
                var gold = new ItemInstance()
                {
                    ItemId = goldId,
                    ContainerId = character.Equipment.BackpackInstanceId,
                    EquipmentId = character.Equipment.EquipmentId,
                    Quantity = quantity
                };
                context.ItemInstances.Add(gold);
            }
            else
            {
                var gold = character.Equipment.ItemInstances.FirstOrDefault(ii => ii.ItemId == goldId && ii.ContainerId == character.Equipment.BackpackInstanceId);
                gold.Quantity = gold.Quantity + quantity;
                context.Update(gold);
            }
            character.CurrentCapacity = character.CurrentCapacity + item.Item.Weight - (quantity * coinWeight);
            if (character.CurrentCapacity > character.MaxCapacity)
            {
                character.CurrentCapacity = character.MaxCapacity;
            }
            context.Remove(character.Equipment.ItemInstances.FirstOrDefault(ii => ii.ItemId == itemId && ii.ContainerId == character.Equipment.BackpackInstanceId));
            context.Update(character);
            context.SaveChanges();
            return quantity;
        }
        public bool IsInBp(Character character, int itemId)
        {
            bool isInBp = character.Equipment.ItemInstances.Any(ii => ii.ItemId == itemId && ii.ContainerId == character.Equipment.BackpackInstanceId);
            return isInBp;
        }
        public bool IsNpcBuying(int npcId, int itemId)
        {
            var npc = npcRepository.GetWithItems(npcId);
            bool isNpcBuying = npc.ItemNpcs.Any(itn => itn.ItemId == itemId);
            return isNpcBuying;
        }
        public Character SetLevel(Character character)
        {
            var expLvlUp = 100;
            var levelBefore = character.Lvl;
            if (character.Experience >= 100)
            {
                character.Lvl = Convert.ToInt32(Math.Floor(Math.Log2(character.Experience / 100))) + 2;
            }
            if (character.Lvl != levelBefore)
            {
                var levelDiff = character.Lvl - levelBefore;
                character.MaxCapacity = character.MaxCapacity + (levelDiff * 10);
                character.CurrentCapacity = character.CurrentCapacity + (levelDiff * 10);
            }
            return character;
        }
    }
}