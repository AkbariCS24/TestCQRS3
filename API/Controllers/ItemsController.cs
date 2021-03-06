using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Commands.Items;
using TestCQRS3.Application.Query.Queries.Item;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.API.Controllers
{
    [Route("api/v{v:apiVersion}/items")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ItemQueryModel>> Get()
        {
            return await _mediator.Send(new GetItemListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ItemQueryModel> Get(int id)
        {
            return await _mediator.Send(new GetItemByIdQuery(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateItemCommand>> Create([FromBody] CreateItemCommand createItemCommand)
        {
            return await _mediator.Send(createItemCommand);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateItemCommand updateItemCommand)
        {
            await _mediator.Send(updateItemCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteItemCommand(id));
            return NoContent();
        }
    }
}
