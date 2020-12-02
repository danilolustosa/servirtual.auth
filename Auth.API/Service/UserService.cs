using Auth.API.Configuration;
using Auth.API.Model;
using Auth.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.API.Service
{
    public class UserService
    {
        public UserRepository _repository = new UserRepository();        

        public User Login(string email, string password)
        {
            var user = _repository.Login(email, password);

            if (user.Id == 0)
                return new User() { StatusCode = StatusCodes.Status404NotFound, Message = "User/Password invalid" };

            user.Token = GenerateToken(user);

            user.StatusCode = StatusCodes.Status200OK;
            return user;
        }

        public User Get(int id)
        {
            var result = _repository.Get(id);
            result.StatusCode = 200;
            return result;
        }

        public List<User> List()
        {
            return _repository.List();
        }



        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString().Trim()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)                    
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
