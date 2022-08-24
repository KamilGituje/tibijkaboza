using AutoMapper;
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

        [HttpPut("killmonster/{monsterId}")]
        public async Task<ActionResult<List<Item>>> KillMonster(int characterId, int monsterId)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var monster = await monsterRepository.GetWithItemsAsync(monsterId);
            var loot = await characterService.KillMonsterAsync(character, monster);
            await characterService.GetLootAsync(character, loot);
            await characterRepository.SaveChangesAsync();
            return Ok(mapper.Map<List<ItemDto>>(loot));
        }
        [HttpPut("sellitem/{npcName}/{itemName}")]
        public async Task<ActionResult<string>> SellItem(int characterId, string npcName, string itemName)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var item = await itemRepository.GetByNameAsync(itemName);
            if (!characterService.IsInBp(character, item))
            {
                return BadRequest();
            }
            var npc = await npcRepository.GetWithItemsByNameAsync(npcName);
            if (!characterService.IsNpcBuying(npc, item))
            {
                return BadRequest();
            }

            await characterService.SellItemAsync(npc, item, character);
            await characterRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}