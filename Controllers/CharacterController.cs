using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    
    [ApiController]                                     //enables api attributes like routing
    [Route("api/[controller]")]                         //how were able to find a specific controller

    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
            
        }

        //[HttpGet]                                       //allows swagger to identify the below text as a GetHTTPRequest
        [HttpGet("GetAll")]                               //swagger requires a name for buttons when theyre are more than one
                                                        // you can combine the Route and HTTPGet
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));      //returns the first value with the Id == id
        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            
            return Ok(await _characterService.AddCharacter(newCharacter));

        }
    }
}
