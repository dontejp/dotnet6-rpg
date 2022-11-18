using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)                  //returns a string that holds the User ID
        {
            var response = new ServiceResponse<string>();                                                   //creating the promised return type
            var user = await _context.Users                                                                 //searching the database Users table for the first person that shows up as the given username...
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user == null)                                                                                //if the user var is still null after going through the search it displays the error message user not found 
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(!VerifyPasswordHash(password,user.PasswordHash, user.PasswordSalt))                     //if the user is found we then verify the password
            {
                response.Success = false;
                response.Message = "Wrong Password.";
            }
            else                                                                                            //if both cases are false then we recieved the correct information therefore we can return the userID as promised            
            {
                response.Data = user.Id.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response; 
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))              //u refers to every single user... then we access the attribute from that user
            {
                return true;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));           //if this returns false the password is wrong
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}