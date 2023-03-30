using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleRESTfulAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySimpleStoreContext _storeContext;
        public AuthController(IConfiguration configuration, MySimpleStoreContext context)
        {
            _configuration = configuration;
            _storeContext = context;
        }
        [HttpPost(Name = "Login")]
        public ActionResult<string> GetToken(LoginVM loginVM)
        {
            var user = _storeContext.Users.FirstOrDefault(u => u.Username == loginVM.Username && u.Password == loginVM.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim (ClaimTypes.Name, user.Username),
                new Claim (ClaimTypes.Role, user.Role)
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);
        }
    }
}
