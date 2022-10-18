using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TibiaAPI.Models;
using TibiaModels.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaAPI.Controllers
{
    [Route("api/npcs")]
    [ApiController]
    public class NpcController : ControllerBase
    {
        public NpcController(INpcRepository _npcRepository, IMapper _mapper)
        {
            npcRepository = _npcRepository;
            mapper = _mapper;
        }
        private readonly INpcRepository npcRepository;
        private readonly IMapper mapper;

        [HttpGet]
        public async Task<ActionResult<List<NpcDto>>> GetNpcsAsync()
        {
            var npcs = await npcRepository.GetNpcsAsync();
            return Ok(mapper.Map<List<NpcDto>>(npcs));
        }
        [HttpGet("{npcId}")]
        public async Task<ActionResult<NpcWithItemsDto>> GetNpcAsync(int npcId)
        {
            var npc = await npcRepository.GetWithItemsAsync(npcId);
            var npcWithItemsDto = mapper.Map<NpcWithItemsDto>(npc);
            var items = new List<ItemWithPriceDto>();
            foreach(var item in npc.ItemNpcs)
            {
                var itemWithPrice = new ItemWithPriceDto()
                {
                    ItemId = item.ItemId,
                    Name = item.Item.Name,
                    Price = item.Price
                };
                items.Add(itemWithPrice);
            }
            npcWithItemsDto.Items = items;
            return Ok(npcWithItemsDto);
        }
    }
}
