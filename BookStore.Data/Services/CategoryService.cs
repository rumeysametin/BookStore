using BookStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result= _context.categories.Add(category);
            _context.SaveChanges();

            return result.Entity;
        }
    }
}
