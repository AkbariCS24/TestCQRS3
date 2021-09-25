using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Domain.Events.Item;

namespace TestCQRS3.Application.Command.Commands.Items
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IItemService _service;
        private readonly IMediator _mediator;

        public UpdateItemCommandHandler(IItemService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Item item = new()
                {
                    Id = request.Id,
                    Field1 = request.Field1,
                    Field2 = request.Field2,
                    Field3 = request.Field3
                };
                _service.Update(item);

                ItemUpdatedEvent Event = new(item);
                _mediator.Publish(Event);

                return Unit.Task;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
    }
}
