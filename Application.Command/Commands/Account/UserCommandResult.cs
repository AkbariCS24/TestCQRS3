using MediatR;

namespace TestCQRS3.Application.Command.Commands.Account
{
    public record UserCommandResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
