using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.Application.Query.Queries.Item2
{
    public record GetItem2ListQuery() : IRequest<List<Item2QueryModel>>;

    public class GetItem2ListQueryHandler : IRequestHandler<GetItem2ListQuery, List<Item2QueryModel>>
    {
        private readonly ReadDBContext _context;

        public GetItem2ListQueryHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task<List<Item2QueryModel>> Handle(GetItem2ListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Item2.Find(item => true).ToList());
        }
    }
}
