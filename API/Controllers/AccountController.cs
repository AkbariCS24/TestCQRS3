using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Commands.Account;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IConfiguration _config;

        public AccountController(IMediator mediator, IConfiguration config)
        {
            _mediator = mediator;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginCommand userLogin)
        {
            IActionResult response = Unauthorized();
            UserCommandResult user = _mediator.Send(userLogin).Result;

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register([FromBody] UserRegisterCommand userRegister)
        {
            IActionResult response = Unauthorized();
            UserCommandResult user = _mediator.Send(userRegister).Result;

            if (user != null)
            {
                response = Ok(new { user = userRegister });
            }
        
            return response;
        }

        private string GenerateJSONWebToken(UserCommandResult userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}