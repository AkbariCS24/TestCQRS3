using MediatR;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.Application.Query.Queries.Item2
{
    public record GetItem2ByIdQuery(int Id) : IRequest<Item2QueryModel>;

    public class GetItem2ByIdQueryHandler : IRequestHandler<GetItem2ByIdQuery, Item2QueryModel>
    {
        private readonly ReadDBContext _context;

        public GetItem2ByIdQueryHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task<Item2QueryModel> Handle(GetItem2ByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Item2.Find(p => p.SqlId == request.Id).FirstOrDefault());
        }
    }
}
