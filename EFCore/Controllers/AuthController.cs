using EFCore.Application.Dtos.UserDtos;
using EFCore.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(IOptionsMonitor<JwtConfig> monitor, UserManager<IdentityUser> userManager)
        {
            _jwtConfig = monitor.CurrentValue;
            _userManager = userManager;
        }

        [HttpPost("Register")]    
        public async Task<ActionResult<UserRegistrationResponseDto>> Register(UserRegistrationDto user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser is null)
                {
                    var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                    var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                    if(isCreated.Succeeded)
                    {
                        var token = GenerateJwtToken(newUser);
                        return Ok(new UserRegistrationResponseDto
                        {
                            Success = false,
                            Token = token
                        });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return BadRequest();

            }

            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLogingRequestDto loginRequest)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);
                if(existingUser is null)
                {
                    return BadRequest("Invalid Login Request");
                }

                var isCorrectPassword = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
                if(isCorrectPassword)
                {
                    var token = GenerateJwtToken(existingUser);
                    return Ok(new UserRegistrationResponseDto() { Success = true, Token = token });
                }
                else
                {
                    return Unauthorized();
                }

            }

            return BadRequest();
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescrioptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescrioptor);
            var JwtToken = jwtTokenHandler.WriteToken(token);

            return JwtToken;
        }

    }
}
