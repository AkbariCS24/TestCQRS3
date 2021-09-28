using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Command.Commands.Item2s
{
    public class DeleteItem2CommandHandler : IRequestHandler<DeleteItem2Command, bool>
    {
        private readonly IServiceWrapper _service;
        private readonly IMediator _mediator;

        public DeleteItem2CommandHandler(IServiceWrapper service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<bool> Handle(DeleteItem2Command request, CancellationToken cancellationToken)
        {
            try
            {
                _service.Item2.Delete(request.Id);

                Item2DeletedEvent Event = new(request.Id);
                _mediator.Publish(Event);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
