using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character { Id =1, 
                            Name = "Sam" 
                            }
        };
        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResposne = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);
            serviceResposne.Data = characters;
            return serviceResposne;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            
            return new ServiceResponse<List<Character>> {Data = characters};
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            var character  = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;
        }
    }
}