using BookStore.Data;
using BookStore.Data.Models;
using BookStore.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business
{
    public class CategoryLogic
    {
        private readonly BookStoreContext _context;

        public CategoryLogic(BookStoreContext context)
        {
            _context = context;
        }

        public int Add(Category category)
        {
            var services = new CategoryService(_context);

            var result=services.Add(category);
            return result.Id;

        }
    }
}
