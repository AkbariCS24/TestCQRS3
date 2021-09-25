using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Domain.Events.Item;

namespace TestCQRS3.Application.Command.Commands.Items
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemCommand>
    {
        private readonly IItemService _service;
        private readonly IMediator _mediator;

        public CreateItemCommandHandler(IItemService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<CreateItemCommand> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Item item = new()
                {
                    Field1 = request.Field1,
                    Field2 = request.Field2,
                    Field3 = request.Field3
                };
                _service.Add(item);
                request.Id = item.Id;

                ItemAddedEvent Event = new(item);
                _mediator.Publish(Event);

                return Task.FromResult(request);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
    }
}