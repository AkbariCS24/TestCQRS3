using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestCQRS3.Application.Command.Commands.Item2s;
using TestCQRS3.Application.Query.Queries.Item2;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.API.Controllers
{
    [Route("api/item2s")]
    [ApiController]
    [Authorize]
    public class Item2sController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Item2sController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Item2QueryModel>> Get()
        {
            return await _mediator.Send(new GetItem2ListQuery());
        }

        [HttpGet("{id}")]
        public async Task<Item2QueryModel> Get(int id)
        {
            return await _mediator.Send(new GetItem2ByIdQuery(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CreateItem2Command>> Create([FromBody] CreateItem2Command createItem2Command)
        {
            return await _mediator.Send(createItem2Command);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateItem2Command updateItem2Command)
        {
            await _mediator.Send(updateItem2Command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteItem2Command(id));
            return NoContent();
        }
    }
}
