using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.Application.Query.Queries.Item
{
    public record GetItemListQuery() : IRequest<List<ItemQueryModel>>;

    public class GetItemListQueryHandler : IRequestHandler<GetItemListQuery, List<ItemQueryModel>>
    {
        private readonly ReadDBContext _context;

        public GetItemListQueryHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task<List<ItemQueryModel>> Handle(GetItemListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Item.Find(item => true).ToList());
        }
    }
}
