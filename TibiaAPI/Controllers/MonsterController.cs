using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TibiaAPI.Models;
using TibiaRepositories.BL;
using TibiaRepositories.BL.Interfaces;

namespace TibiaAPI.Controllers
{
    [Route("api/monsters")]
    [ApiController]
    public class MonsterController : ControllerBase
    {
        public MonsterController(IMonsterRepository _monsterRepository, IMapper _mapper)
        {
            monsterRepository = _monsterRepository;
            mapper = _mapper;
        }
        private readonly IMonsterRepository monsterRepository;
        private readonly IMapper mapper;

        [HttpGet]
        public async Task<ActionResult<List<MonsterDto>>> GetMonstersAsync()
        {
            var monsters = await monsterRepository.GetMonstersAsync();
            return Ok(mapper.Map<List<MonsterDto>>(monsters));
        }
    }
}
