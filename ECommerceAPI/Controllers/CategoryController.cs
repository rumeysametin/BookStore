using BookStore.Business;
using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly BookStoreContext _context;

        public CategoryController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public int Add([FromBody] Category category)
        {
            var logic = new CategoryLogic(_context);
            var result = logic.Add(category);
            return result;
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] Category category)
        {
            var logic = new CategoryLogic(_context);
            var success = logic.Update(category);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var logic = new CategoryLogic(_context);
            var success = logic.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetById(int id)
        {
            var logic = new CategoryLogic(_context);
            var category = logic.GetById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            var logic = new CategoryLogic(_context);
            var categories = logic.GetAll();
            return Ok(categories);
        }
    }
}
