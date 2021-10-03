using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCQRS3.Application.Command.Commands.Account;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;
using TestCQRS3.API.Helpers;

namespace TestCQRS3.API.Controllers
{
    [Route("api/v{v:apiVersion}/account")]
    [ApiVersion("1.0")]
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
                var tokenString = WebToken.GenerateJSONWebToken(_config, user.UserName, user.Password, user.Role);
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
    }
}