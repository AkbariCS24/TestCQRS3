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
        private readonly IServiceWrapper _service;
        private readonly IMediator _mediator;

        public CreateItemCommandHandler(IServiceWrapper service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<CreateItemCommand> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = Item.CreateNewItem(request.Field1, request.Field2, request.Field3);
                _service.Item.Add(item);
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