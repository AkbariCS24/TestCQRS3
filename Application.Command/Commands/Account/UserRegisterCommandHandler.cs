using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Common;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Application.Command.Commands.Account
{
    class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserCommandResult>
    {
        private readonly ITestCQRS3DBContext _context;

        public UserRegisterCommandHandler(ITestCQRS3DBContext context)
        {
            _context = context;
        }

        public Task<UserCommandResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = new()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    Email = request.Email,
                    UserRoleId = 2
                };
                _context.Users.Add(user);
                _context.Save();

                UserCommandResult userResult = new()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    Email = request.Email
                };
                return Task.FromResult(userResult);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
