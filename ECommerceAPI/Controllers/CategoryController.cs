using BookStore.Business;
using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:Controller
    {
        private readonly BookStoreContext _context;

        public CategoryController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public int Add([FromBody]Category category)
        {
            var logic = new CategoryLogic(_context);
            var result = logic.Add(category);

            return result;
        }
    }
}
