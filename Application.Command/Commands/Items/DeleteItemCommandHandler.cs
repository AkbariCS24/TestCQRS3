using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Events.Item;

namespace TestCQRS3.Application.Command.Commands.Items
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly IItemService _service;
        private readonly IMediator _mediator;

        public DeleteItemCommandHandler(IItemService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        public Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _service.Delete(request.Id);

                ItemDeletedEvent Event = new(request.Id);
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
