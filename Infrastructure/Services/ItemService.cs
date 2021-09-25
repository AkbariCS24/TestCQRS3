using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Entities;
using TestCQRS3.Infrastructure.Persistence;

namespace TestCQRS3.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly TestCQRS3DBContext _context;

        public ItemService(TestCQRS3DBContext context)
        {
            _context = context;
        }

        public List<Item> Get() =>
                _context.Items.ToList();


        public Item Get(int Id) =>
                _context.Items.Find(Id);

        public Item Add(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(int Id)
        {
            try
            {
                var item = _context.Items.Find(Id);
                _context.Items.Remove(item);
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
            if (_context.Items.Any(e => e.Id == Id))
                return true;
            else
                return false;
        }
    }
}
