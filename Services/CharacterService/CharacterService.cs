using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

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
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResposne = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);                             //Since the input type was changed to AddCharacterDTO from Character... we now need to map the newCharacter to Character since they have different params
            character.Id = characters.Max(c => c.Id) +1;                                            // returns the max characterID then adds 1
            characters.Add(character);
            serviceResposne.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();    //this is being done because the we still need to map the character into a GetCharDTO before we put it into the serviceResponse
            return serviceResposne;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            
            return new ServiceResponse<List<GetCharacterDto>> {
                Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
                };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character  = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);                     //this is being mapped... <class its being mapped to>(what is being mapped);

            return serviceResponse;
        }
    }
}