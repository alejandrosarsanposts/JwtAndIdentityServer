using ApiPrincipal.Helpers;
using ApiPrincipal.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace ApiPrincipal.Services
{
    public interface ILoginService
    {
        LoginResponse Authenticate(LoginModel model);
    }

    public class LoginService : ILoginService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User {  Id = 1, FirstName = "Nombre", LastName = "Apellido", Username = "user", Password = "user" }
        };

        private readonly IConfiguration _config;
        private string jwtKey;

        public LoginService(IConfiguration config)
        {
            _config = config;
            jwtKey = _config.GetSection("JwtSettings:Secret").Value;
        }

        public LoginResponse Authenticate(LoginModel model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);            
            if (user == null) return null;            
            var token = GenerateJwtToken(user);

            return new LoginResponse(user, token);
        } 
        private string GenerateJwtToken(User user)
        {
            var tokenhandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var keys = System.Text.Encoding.ASCII.GetBytes(jwtKey);
            var tokendescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keys),
                    Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)

            };
            var tokens = tokenhandler.CreateToken(tokendescriptor);

            return tokenhandler.WriteToken(tokens);

            
        }
    }
}
