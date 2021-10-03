using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Command.Commands.Item2s
{
    public class CreateItem2CommandHandler : IRequestHandler<CreateItem2Command, CreateItem2Command>
    {
        private readonly IServiceWrapper _service;
        private readonly IMediator _mediator;

        public CreateItem2CommandHandler(IServiceWrapper service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<CreateItem2Command> Handle(CreateItem2Command request, CancellationToken cancellationToken)
        {
            try
            {
                var item2 = Item2.CreateNewItem2(
                    request.ItemId,
                    request.Field1,
                    request.Field2,
                    request.Field3);

                _service.Item2.Add(item2);
                request.Id = item2.Id;

                Item2AddedEvent Event = new(item2);
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