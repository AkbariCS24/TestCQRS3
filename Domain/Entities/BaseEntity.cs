using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCQRS3.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; }
        public bool IsActive { get; set; }
    }
}
