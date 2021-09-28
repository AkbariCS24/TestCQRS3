using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Common;

namespace TestCQRS3.Application.Command.Commands.Account
{
    class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserCommandResult>
    {
        private readonly ITestCQRS3DBContext _context;

        public UserLoginCommandHandler(ITestCQRS3DBContext context)
        {
            _context = context;
        }

        public Task<UserCommandResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.Include(p => p.UserRole).FirstOrDefault(p => p.UserName == request.UserName && p.Password == request.Password);
            if (user != null)
            {
                UserCommandResult userCommandResult = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.UserRole.Title
                };
                return Task.FromResult(userCommandResult);
            }
            else
            {
                return null;
            }

        }
    }
}
