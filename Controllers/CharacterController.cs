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
        private static Character knight = new Character();

        [HttpGet]                                       //allows swagger to identify the below text as a GetHTTPRequest
        public ActionResult<Character> Get()
        {
            return Ok(knight);
        }
    }
}