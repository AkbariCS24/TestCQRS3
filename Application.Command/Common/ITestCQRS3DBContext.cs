using Microsoft.EntityFrameworkCore;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Application.Command.Common
{
    public interface ITestCQRS3DBContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Item2> Item2s { get; set; }
        public DbSet<User> Users { get; set; }

        public void Save();
    }
}
