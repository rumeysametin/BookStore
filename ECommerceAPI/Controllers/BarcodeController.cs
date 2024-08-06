using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using BookStore.Data.Models;
using BookStore.Data.Services;
using System.Drawing;
using ZXing.Windows.Compatibility;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly BookInfoService _bookInfoService;

        public BarcodeController(BookInfoService bookInfoService)
        {
            _bookInfoService = bookInfoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBarcodeImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya seçilmedi.");
            }

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var barcodeBitmap = new Bitmap(stream);
                var barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(barcodeBitmap);

                if (result == null)
                {
                    return BadRequest("Barkod okunamadı.");
                }

                var isbn = result.Text;
                var bookInfo = await _bookInfoService.GetBookInfoByIsbnAsync(isbn);

                return Ok(bookInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetBookInfoByIsbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return BadRequest("ISBN kodu geçersiz.");
            }

            try
            {
                var bookInfo = await _bookInfoService.GetBookInfoByIsbnAsync(isbn);
                return Ok(bookInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
