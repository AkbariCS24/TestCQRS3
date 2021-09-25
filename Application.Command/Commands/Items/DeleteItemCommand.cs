using MediatR;

namespace TestCQRS3.Application.Command.Commands.Items
{
    public record DeleteItemCommand(int Id) : IRequest<bool>;
}