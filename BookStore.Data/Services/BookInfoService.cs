using System;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Data.Models;
using Newtonsoft.Json.Linq;

namespace BookStore.Data.Services
{
    public class BookInfoService
    {
        private readonly HttpClient _httpClient;

        public BookInfoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BarcodeBook> GetBookInfoByIsbnAsync(string isbn)
        {
            var url = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&format=json&jscmd=data";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var bookInfo = json[$"ISBN:{isbn}"];

            if (bookInfo == null)
            {
                throw new Exception("Kitap bilgileri bulunamadı.");
            }

            return new BarcodeBook
            {
                Isbn = isbn,
                Title = bookInfo["title"]?.ToString(),
                Authors = string.Join(", ", bookInfo["authors"]?.Select(a => a["name"].ToString())),
                Publishers = string.Join(", ", bookInfo["publishers"]?.Select(p => p["name"].ToString())),
                PublishDate = bookInfo["publish_date"]?.ToString(),
                EditionCount = int.TryParse(bookInfo["number_of_pages"]?.ToString(), out var pages) ? pages : (int?)null
            };
        }
    }
}
