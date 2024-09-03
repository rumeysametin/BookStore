using BookStore.Data.Models;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Logic
{
    public class ProductLogic
    {
        private readonly BookStoreContext _context;

        public ProductLogic(BookStoreContext context)
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
