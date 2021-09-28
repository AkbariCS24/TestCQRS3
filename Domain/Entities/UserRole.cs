using System.Collections.Generic;

namespace TestCQRS3.Domain.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
