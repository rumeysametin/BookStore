using BookStore.Data.Models;
using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly BookStoreContext _context;

        public ProductController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet("items")]
        public async Task<ActionResult<List<Product>>> GetCartItems()
            {
            var items = await _context.Products.ToListAsync();
            return Ok(items);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProductToCart([FromBody] Product product)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                await _context.Products.AddAsync(product);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProductQuantity([FromBody] ProductUpdateModel updateModel)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == updateModel.ProductId);

            if (product != null)
            {
                product.Quantity = updateModel.Quantity;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("total")]
        public async Task<ActionResult<decimal>> GetTotalCost()
        {
            var totalCost = await _context.Products.SumAsync(p => p.Price * p.Quantity);
            return Ok(totalCost);
        }
    }

    public class ProductUpdateModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
