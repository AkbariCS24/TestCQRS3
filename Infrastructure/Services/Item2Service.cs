using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Infrastructure.Persistence;

namespace TestCQRS3.Infrastructure.Services
{
    public class Item2Service : IItem2Service
    {
        private readonly TestCQRS3DBContext _context;

        public Item2Service(TestCQRS3DBContext context)
        {
            _context = context;
        }

        public List<Item2> Get() =>
                _context.Item2s.ToList();


        public Item2 Get(int Id) =>
                _context.Item2s.Find(Id);

        public Item2 Add(Item2 item2)
        {
            _context.Item2s.Add(item2);
            _context.SaveChanges();
            return item2;
        }

        public void Update(Item2 item2)
        {
            _context.Entry(item2).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(int Id)
        {
            try
            {
                var item2 = _context.Item2s.Find(Id);
                _context.Item2s.Remove(item2);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsExists(int Id)
        {
            if (_context.Item2s.Any(e => e.Id == Id))
                return true;
            else
                return false;
        }
    }
}
