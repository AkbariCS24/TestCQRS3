using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Command.Commands.Item2s
{
    public class UpdateItem2CommandHandler : IRequestHandler<UpdateItem2Command>
    {
        private readonly IServiceWrapper _service;
        private readonly IMediator _mediator;

        public UpdateItem2CommandHandler(IServiceWrapper service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<Unit> Handle(UpdateItem2Command request, CancellationToken cancellationToken)
        {
            try
            {
                var item2 = Item2.CreateItem2(
                    request.Id,
                    request.ItemId,
                    request.Field1,
                    request.Field2,
                    request.Field3);

                _service.Item2.Update(item2);

                Item2UpdatedEvent Event = new(item2);
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
