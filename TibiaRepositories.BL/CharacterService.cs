﻿using AutoMapper;
using DB1;
using Microsoft.EntityFrameworkCore;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaRepositories.BL
{
    public class CharacterService : ICharacterService
    {
        public CharacterService(ICharacterRepository _characterRepository, IItemRepository _itemRepository, IItemInstanceRepository _itemInstanceRepository, IUserRepository _userRepository)
        {
            characterRepository = _characterRepository;
            itemRepository = _itemRepository;
            itemInstanceRepository = _itemInstanceRepository;
            userRepository = _userRepository;
        }
        private readonly ICharacterRepository characterRepository;
        private readonly IItemRepository itemRepository;
        private readonly IItemInstanceRepository itemInstanceRepository;
        private readonly IUserRepository userRepository;

        public bool IsValid(Character character)
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
        public async Task<Character> CreateAsync(Character character, Guid userId)
        {
            if(!IsValid(character))
            {
                return null;
            }
            var user = await userRepository.GetAsync(userId);
            SetLevel(character);
            character.UserId = userId;
            await characterRepository.AddAsync(character);
            var backpackId = (await itemRepository.GetByNameAsync("Jacula backpack")).ItemId;
            var backpack = new ItemInstance
            {
                ItemId = backpackId,
                EquipmentId = character.Equipment.EquipmentId,
            };
            var backpackInstanceId = await itemInstanceRepository.AddAsync(backpack);
            character.Equipment.BackpackInstanceId = backpackInstanceId;
            await characterRepository.SaveChangesAsync();
            return character;
        }
        public async Task<List<Item>> KillMonsterAsync(Character character, Monster monster)
        {
            character.Experience = character.Experience + monster.Experience;
            var loot = RandomLoot(monster.ItemMonsters);
            SetLevel(character);
            await characterRepository.SaveChangesAsync();
            return loot;
        }
        public async Task<List<Item>> GetLootAsync(Character character, List<Item> loot)
        {
            var itemsCarried = new List<Item>();
            foreach (var item in loot)
            {
                if (item.Weight <= character.CurrentCapacity)
                {
                    var itemInstance = new ItemInstance()
                    {
                        ItemId = item.ItemId,
                        EquipmentId = character.Equipment.EquipmentId,
                        ContainerId = character.Equipment.BackpackInstanceId,
                        Quantity = 1
                    };
                    await itemInstanceRepository.AddAsync(itemInstance);
                    character.CurrentCapacity = character.CurrentCapacity - item.Weight;
                    itemsCarried.Add(item);
                }
            }
            await characterRepository.SaveChangesAsync();
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
        public async Task<int> SellItemAsync(Npc npc, Item item, Character character)
        {
            var goldId = (await itemRepository.GetByNameAsync("Gold coin")).ItemId;
            bool hasCharMoney = character.Equipment.ItemInstances.Any(ii => ii.ItemId == goldId && ii.ContainerId == character.Equipment.BackpackInstanceId);
            var coinWeight = (await itemRepository.GetAsync(goldId)).Weight;
            var quantity = npc.ItemNpcs.FirstOrDefault(itn => itn.ItemId == item.ItemId).Price;

            if (!hasCharMoney)
            {
                var gold = new ItemInstance()
                {
                    ItemId = goldId,
                    ContainerId = character.Equipment.BackpackInstanceId,
                    EquipmentId = character.Equipment.EquipmentId,
                    Quantity = quantity
                };
                await itemInstanceRepository.AddAsync(gold);
            }
            else
            {
                var gold = character.Equipment.ItemInstances.FirstOrDefault(ii => ii.ItemId == goldId && ii.ContainerId == character.Equipment.BackpackInstanceId);
                gold.Quantity = gold.Quantity + quantity;
            }
            character.CurrentCapacity = character.CurrentCapacity + item.Weight - (quantity * coinWeight);
            if (character.CurrentCapacity > character.MaxCapacity)
            {
                character.CurrentCapacity = character.MaxCapacity;
            }
            await characterRepository.RemoveItemFromBackpackAsync(character, item);

            return quantity;
        }
        public bool IsInBp(Character character, Item item)
        {
            return character.Equipment.ItemInstances.Any(ii => ii.ItemId == item.ItemId && ii.ContainerId == character.Equipment.BackpackInstanceId);
        }
        public bool IsNpcBuying(Npc npc, Item item)
        {
            return npc.ItemNpcs.Any(itn => itn.ItemId == item.ItemId);
        }
        public Character SetLevel(Character character)
        {
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
            if (character.Lvl == 0)
            {
                character.Lvl++;
                character.CurrentCapacity = 500;
                character.MaxCapacity = 500;
            }
            return character;
        }
        public List<Item> GetCharacterItemsInBp(Character character)
        {
            var items = character.Equipment.ItemInstances.Where(ii => ii.ContainerId == character.Equipment.BackpackInstanceId).ToList();
            var itemsToReturn = new List<Item>();
            foreach(var item in items)
            {
                itemsToReturn.Add(new Item
                {
                    ItemId = item.Item.ItemId,
                    Name = item.Item.Name,
                    Weight = item.Item.Weight * item.Quantity,
                    Quantity = item.Quantity
                });
            }
            return itemsToReturn;
        }
    }
}