using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Models
{
    public class BarcodeBook
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Publishers { get; set; }
        public string PublishDate { get; set; }
        public int? EditionCount { get; set; }
    }
}
