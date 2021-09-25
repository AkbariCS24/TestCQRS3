using Microsoft.EntityFrameworkCore;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Infrastructure.Persistence
{
    public class TestCQRS3DBContext : DbContext
    {
        public TestCQRS3DBContext(DbContextOptions<TestCQRS3DBContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
