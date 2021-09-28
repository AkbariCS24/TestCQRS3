using TestCQRS3.Domain.Contracts;
using TestCQRS3.Infrastructure.Persistence;

namespace TestCQRS3.Infrastructure.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly TestCQRS3DBContext _context;
        private ItemService _item;
        private Item2Service _item2;

        public ServiceWrapper(TestCQRS3DBContext context)
        {
            _context = context;
        }

        public IItemService Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new ItemService(_context);
                }

                return _item;
            }
        }

        public IItem2Service Item2
        {
            get
            {
                if (_item2 == null)
                {
                    _item2 = new Item2Service(_context);
                }

                return _item2;
            }
        }
    }
}
