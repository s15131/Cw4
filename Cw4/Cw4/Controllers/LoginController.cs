using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cw4.DTOs;
using Cw4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cw4.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : Controller
    {
        public IConfiguration Configuration { get; set; }
        private IStudentDbService service;
        public LoginController(IConfiguration configuration, IStudentDbService service)
        {
            configuration = configuration;
            service = service;
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto request)
        {
           

          

            var claims = new[] {
             new Claim(ClaimTypes.NameIdentifier, request.Index),
             new Claim(ClaimTypes.Name, service.GetName(request.Index)),
             new Claim(ClaimTypes.Role, "student")
             };

            /* var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "ala"),
            new Claim(ClaimTypes.Role, "student")
            };
            */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: "s15131",
                audience: "Students",
                claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });

        }
    }
}