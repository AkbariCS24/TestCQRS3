using MediatR;

namespace TestCQRS3.Application.Command.Commands.Account
{
    public record UserLoginCommand : IRequest<UserCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
