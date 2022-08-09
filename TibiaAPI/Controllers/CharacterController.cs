using AutoMapper;
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
        public async Task<ActionResult<CharacterWithoutEquipmentDto>> GetCharacter(int characterId, bool includeEquipment = false)
        {
            if(includeEquipment)
            {
                var characterWithEquipment = await characterRepository.GetWithItemsAsync(characterId);
                return Ok(mapper.Map<CharacterDto>(characterWithEquipment));
            }
            var character = await characterRepository.GetAsync(characterId);
            return Ok(mapper.Map<CharacterWithoutEquipmentDto>(character));
        }
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> CreateCharacter(CharacterForCreationDto character)
        {
            var characterToAdd = await characterService.CreateAsync(mapper.Map<Character>(character));
            var characterAdded = mapper.Map<CharacterWithoutEquipmentDto>(characterToAdd);
            return CreatedAtRoute("GetCharacter",
                new
                {
                    characterId = characterAdded.CharacterId
                },
                characterAdded);
        }
        [HttpGet("{characterId}/backpack")]
        public async Task<ActionResult<List<ItemWithQuantityDto>>> GetCharacterItemsInBp(int characterId)
        {
            var character = await characterRepository.GetWithItemsAsync(characterId);
            var items = characterService.GetCharacterItemsInBp(character);
            return Ok(mapper.Map<List<ItemWithQuantityDto>>(items));
        }
    }
}