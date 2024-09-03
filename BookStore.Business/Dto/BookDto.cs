using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Dto
{
        public class BookDto
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Publishers { get; set; }
        public string PublishDate { get; set; }
        public int PageCount { get; set; }
        public string Explanation { get; set; }
        public int? CategoryId { get; set; }
        public byte[]? CoverImage { get; set; }

        public IFormFile UploadFile { get; set; }           

    }
}
