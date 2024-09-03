using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; } 
        public string Publishers { get; set; } 
        public decimal Price { get; set; } 
        public int Quantity { get; set; }

    }
}
