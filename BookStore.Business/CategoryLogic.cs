using BookStore.Data.Models;
using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Services;

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
            var result = services.Add(category);
            return result.Id;
        }

        public bool Update(Category category)
        {
            var services = new CategoryService(_context);
            var existingCategory = services.GetById(category.Id);

            if (existingCategory == null)
                return false;

            existingCategory.Name = category.Name;
            services.Update(existingCategory);
            return true;
        }

        public bool Delete(int id)
        {
            var services = new CategoryService(_context);
            var category = services.GetById(id);

            if (category == null)
                return false;

            services.Delete(id);
            return true;
        }

        public Category GetById(int id)
        {
            var services = new CategoryService(_context);
            return services.GetById(id);
        }

        public List<Category> GetAll()
        {
            var services = new CategoryService(_context);
            return services.GetAll();
        }
    }
}
