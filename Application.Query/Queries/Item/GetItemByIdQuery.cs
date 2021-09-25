using MediatR;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.Application.Query.Queries.Item
{
    public record GetItemByIdQuery(int Id) : IRequest<ItemQueryModel>;

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemQueryModel>
    {
        private readonly ReadDBContext _context;

        public GetItemByIdQueryHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task<ItemQueryModel> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Item.Find(p => p.SqlId == request.Id).FirstOrDefault());
        }
    }
}
