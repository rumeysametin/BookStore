using BookStore.Data;
using BookStore.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class ProductService
    {
        private readonly BookStoreContext _context;

        public ProductService(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetCartItemsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProductToCartAsync(Product product)
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
        }

        public async Task UpdateProductQuantityAsync(int productId, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product != null)
            {
                product.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateTotalCostAsync()
        {
            return await _context.Products.SumAsync(p => p.Price * p.Quantity);
        }
    }
}
