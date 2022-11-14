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

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();         //QUESTION: the previous way didnt return the rest of the list for some reason so this needed to be changed


            try
            {                                                                                        
                Character character = characters.First(c => c.Id == id);                   //firstordefault returns null , first just returns an exception if not found
                characters.Remove(character);
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)                                                           
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
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

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();         


            try{                                                                                        //exception 
            Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            //_mapper.Map(updatedCharacter, character);                   this maps the updated character andsaves it into the character variable

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }catch (Exception ex)                                                           
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}