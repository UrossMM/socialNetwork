using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace socialNetwork.Repository
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration configuration;


        //ovaj dictionary ce biti obrisan i koristice se podaci iz baze (preko context)
        /*Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1", "password1" },
            {"user2", "password2" },
            {"user3", "password3" },
        };*/

        public JWTManagerService(IConfiguration configuration, AppDbContext context)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(UserDTO user)
        {

            //umesto ovog uslova treba da ide provera iz baze(da li je takav korisnik registrovan)
            /*if(!UserRecords.Any(x => x.Key==user.Name && x.Value==user.Password))
            {
                return null;
            }*/



            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenhandler.WriteToken(token) };
        }

    }
}
