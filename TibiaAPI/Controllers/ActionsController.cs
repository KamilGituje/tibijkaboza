using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using TibiaAPI.Models;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaAPI.Controllers
{
    [Route("api/characters/{characterId}/actions")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        public ActionsController(ICharacterService _characterService, ICharacterRepository _characterRepository, IMonsterRepository _monsterRepository,
            IItemRepository _itemRepository, INpcRepository _npcRepository, IMapper _mapper)
        {
            characterService = _characterService;
            characterRepository = _characterRepository;
            monsterRepository = _monsterRepository;
            itemRepository = _itemRepository;
            npcRepository = _npcRepository;
            mapper = _mapper;
        }
        private readonly ICharacterService characterService;
        private readonly ICharacterRepository characterRepository;
        private readonly IMonsterRepository monsterRepository;
        private readonly IItemRepository itemRepository;
        private readonly INpcRepository npcRepository;
        private readonly IMapper mapper;

        [Authorize(policy: "CanAccessExp")]
        [HttpPut("killmonster/{monsterId}")]
        public async Task<ActionResult<List<Item>>> KillMonsterAsync(int characterId, int monsterId)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var monster = await monsterRepository.GetWithItemsAsync(monsterId);
            var loot = await characterService.KillMonsterAsync(character, monster);
            var lootCarried = await characterService.GetLootAsync(character, loot);
            foreach(var item in loot)
            {
                if(!lootCarried.Contains(item))
                {
                    var itemTooHeavy = new Item()
                    {
                        Name = $"{item.Name} zbyt ciężki"
                    };
                    lootCarried.Add(itemTooHeavy);
                }
            }
            return Ok(mapper.Map<List<ItemDto>>(lootCarried));
        }
        [Authorize(policy: "CanAccessSell")]
        [HttpPut("sellitem/{npcId}/{itemId}")]
        public async Task<ActionResult<int>> SellItemAsync(int characterId, int npcId, int itemId)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var item = await itemRepository.GetAsync(itemId);
            if (!characterService.IsInBp(character, item))
            {
                return BadRequest();
            }
            var npc = await npcRepository.GetWithItemsAsync(npcId);
            if (!characterService.IsNpcBuying(npc, item))
            {
                return BadRequest();
            }
            var price = await characterService.SellItemAsync(npc, item, character);
            return Ok(price);
        }
    }
}