using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using TibiaAPI.Models;
using TibiaModels.BL;
using TibiaModels.BL.Security;
using TibiaRepositories.BL;

namespace TibiaAPI.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        public SecurityController(SecurityManager _manager, IMapper _mapper)
        {
            manager = _manager;
            mapper = _mapper;
        }
        private readonly SecurityManager manager;
        private readonly IMapper mapper;
        [HttpPut("login")]
        public async Task<ActionResult<string>> Login(UserForLoginAndCreationDto user)
        {
            var token = await manager.ValidateUserAsync(user.UserName, user.Password);
            if (token == String.Empty)
            {
                return NotFound();
            }
            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserForLoginAndCreationDto>> CreateUser(UserForLoginAndCreationDto user)
        {
            var userAdded = await manager.CreateUserAsync(mapper.Map<User>(user));
            if (userAdded == null)
            {
                return BadRequest();
            }
            return Ok(userAdded.UserName);
        }
    }
}