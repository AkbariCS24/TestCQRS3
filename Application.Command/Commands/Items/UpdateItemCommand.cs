using MediatR;

namespace TestCQRS3.Application.Command.Commands.Items
{
    public record UpdateItemCommand : IRequest
    {
        public int Id { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
    }
}
