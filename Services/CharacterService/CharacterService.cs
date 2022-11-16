using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResposne = new ServiceResponse<List<GetCharacterDto>>();
            
            Character character = _mapper.Map<Character>(newCharacter);                             //Since the input type was changed to AddCharacterDTO from Character... we now need to map the newCharacter to Character since they have different params
            //character.Id = characters.Max(c => c.Id) +1;                                            // returns the max characterID then adds 1
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResposne.Data = await _context.Characters
            .Select(c => _mapper.Map<GetCharacterDto>(c))
            .ToListAsync();    //this is being done because the we still need to map the character into a GetCharDTO before we put it into the serviceResponse
            return serviceResposne;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();         //QUESTION: the previous way didnt return the rest of the list for some reason so this needed to be changed


            try
            {                                                                                        
                Character character = await _context.Characters.FirstAsync(c => c.Id == id);                   //firstordefault returns null , first just returns an exception if not found
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter  = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);                     //this is being mapped... <class its being mapped to>(what is being mapped);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();         
            

            try{                                                                                        //exception 
            var character = await _context.Characters
            .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

            //_mapper.Map(updatedCharacter, character);                   this maps the updated character andsaves it into the character variable
            //the above code worked but _mapper.Map<Character>(updatedCharacter); didnt ... maybe save this in a variable then set it equal to character?

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            await _context.SaveChangesAsync();

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