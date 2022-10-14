using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TibiaAPI.Models;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaAPI.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        public CharacterController(ICharacterService _characterService, ICharacterRepository _characterRepository, IMapper _mapper)
        {
            characterService = _characterService;
            characterRepository = _characterRepository;
            mapper = _mapper;
        }
        private readonly ICharacterService characterService;
        private readonly ICharacterRepository characterRepository;
        private readonly IMapper mapper;

        [HttpGet("{characterId}", Name = "GetCharacter")]
        public async Task<ActionResult<CharacterWithoutEquipmentDto>> GetCharacterAsync(int characterId, bool includeEquipment = false)
        {
            if(includeEquipment)
            {
                var characterWithEquipment = await characterRepository.GetWithItemsAsync(characterId);
                return Ok(mapper.Map<CharacterDto>(characterWithEquipment));
            }
            var character = await characterRepository.GetAsync(characterId);
            return Ok(mapper.Map<CharacterWithoutEquipmentDto>(character));
        }
        [HttpPost("create/{userId}")]
        public async Task<ActionResult<CharacterDto>> CreateCharacterAsync(CharacterForCreationDto character, Guid userId)
        {
            var characterToAdd = await characterService.CreateAsync(mapper.Map<Character>(character), userId);
            var characterAdded = mapper.Map<CharacterWithoutEquipmentDto>(characterToAdd);
            return CreatedAtRoute("GetCharacter",
                new
                {
                    characterId = characterAdded.CharacterId
                },
                characterAdded);
        }
        [Authorize(policy: "CanAccessBackpack")]
        [HttpGet("{characterId}/backpack")]
        public async Task<ActionResult<List<ItemWithQuantityDto>>> GetCharacterItemsInBpAsync(int characterId)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var items = characterService.GetCharacterItemsInBp(character);
            return Ok(mapper.Map<List<ItemWithQuantityDto>>(items));
        }
        [HttpGet]
        public async Task<ActionResult<List<CharacterWithoutEquipmentDto>>> GetCharactersAsync(int page = 1)
        {
            var characters = await characterRepository.GetCharactersPaged(page);
            return Ok(mapper.Map<List<CharacterWithoutEquipmentDto>>(characters));
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Character>>> GetCharactersForUserAsync(Guid userId)
        {
            var characters = await characterRepository.GetForUserAsync(userId);
            return Ok(mapper.Map<List<CharacterWithoutEquipmentDto>>(characters));
        }
    }
}