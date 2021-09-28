using Microsoft.EntityFrameworkCore;
using TestCQRS3.Application.Command.Common;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Infrastructure.Persistence
{
    public class TestCQRS3DBContext : DbContext, ITestCQRS3DBContext
    {
        public TestCQRS3DBContext(DbContextOptions<TestCQRS3DBContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Item2> Item2s { get; set; }
        public DbSet<User> Users { get; set; }

        public void Save()
        {
            SaveChanges();
        }
    }
}
