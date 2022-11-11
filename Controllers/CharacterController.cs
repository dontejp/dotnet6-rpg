using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    
    [ApiController]                                     //enables api attributes like routing
    [Route("api/[controller]")]                         //how were able to find a specific controller

    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character { Id =1, 
                            Name = "Sam" 
                            }
        };

        //[HttpGet]                                       //allows swagger to identify the below text as a GetHTTPRequest
        [HttpGet("GetAll")]                               //swagger requires a name for buttons when theyre are more than one
                                                        // you can combine the Route and HTTPGet
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));      //returns the first value with the Id == id
        }
    }
}