using MediatR;

namespace TestCQRS3.Application.Command.Commands.Item2s
{
    public record CreateItem2Command : IRequest<CreateItem2Command>
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Field1 { get; set; }
        public bool Field2 { get; set; }
        public string Field3 { get; set; }
    }
}
