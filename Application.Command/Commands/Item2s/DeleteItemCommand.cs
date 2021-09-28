using MediatR;

namespace TestCQRS3.Application.Command.Commands.Item2s
{
    public record DeleteItem2Command(int Id) : IRequest<bool>;
}