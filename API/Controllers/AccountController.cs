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
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;

namespace TestCQRS3.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private IConfiguration _config;

        public AccountController(IMediator mediator, IConfiguration config, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
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
                _logger.Log(LogType.UserLogin, user.UserName, $"loggin date: {DateTime.Now}");
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
                _logger.Log(LogType.UserRegister, user.UserName, $"register date: {DateTime.Now}");
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
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role,userInfo.Role),
            new Claim(ClaimTypes.Name,userInfo.UserName)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}