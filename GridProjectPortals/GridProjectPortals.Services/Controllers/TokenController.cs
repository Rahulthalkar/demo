using GridProjectPortals.BLL;
using GridProjectPortals.DAL.Interface;
using GridProjectPortals.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GridProjectPortals.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly tokenService _tokenService;
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration,ITokenRespository tokenRespository)
        {
            _tokenService=new tokenService(configuration,tokenRespository);
            _configuration = configuration;
        }

        public class FormRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpPost]
        public IActionResult Token([FromBody] FormRequest request)
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var username = Cryptography.Encrypt(request.username);
            var password=Cryptography.Encrypt(request.password);
            var responseToken=_tokenService.GenerateToken(username,password);
            if (responseToken.Value)
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration.GetSection("Jwt:Issuer").Value,
                    _configuration.GetSection("Jwt:Audience").Value,
                claims,
                expires: DateTime.UtcNow.AddMinutes(100),
                    signingCredentials: signIn);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(token);

                var response = new APIResult<string>
                {
                    IsSuccess = true,
                    Value = tokenString
                };
                return Ok(response);
            }
            else
            {
                var errorResponse = new APIResult<string>
                {
                    IsSuccess = false,
                    ErrorMessageKey =responseToken.ExceptionInfo
                };

                return BadRequest(errorResponse);
            }
        }
    }
}
