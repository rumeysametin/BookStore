using BookStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Data.Services
{
    public class CategoryService
    {
        private readonly BookStoreContext _context;

        public CategoryService(BookStoreContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            var result = _context.categories.Add(category);
            _context.SaveChanges();
            return result.Entity;
        }

        public void Update(Category category)
        {
            _context.categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.categories.Find(id);
            if (category != null)
            {
                _context.categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public Category GetById(int id)
        {
            return _context.categories.Find(id);
        }

        public List<Category> GetAll()
        {
            return _context.categories.ToList();
        }
    }
}
