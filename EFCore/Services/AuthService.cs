using EFCore.Application.Interfaces.SeviceInterfaces;
using EFCore.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Services
{
    public class AuthService : IAuthService
    {
        private JwtConfig _jwtConfig;

        public AuthService(IOptionsMonitor<JwtConfig> monitor)
        {
            _jwtConfig = monitor.CurrentValue;
        }
        public string DecodeJWTTOken(string token)
        {
            if(token is not null)
            {
                var jwtEncodedString = token.Substring(7);
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

                var tokenDecrypted = jwtTokenHandler.ReadJwtToken(jwtEncodedString);
                var role = tokenDecrypted.Claims.First(c => c.Type == "role").Value;

                return role;
            }

            return null;
            
        }
    }
}
