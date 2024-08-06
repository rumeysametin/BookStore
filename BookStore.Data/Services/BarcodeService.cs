using ZXing;
using ZXing.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace BookStore.Data.Services
{
    public class BarcodeService
    {
        public string DecodeBarcodeFromImage(string imagePath)
        {
            var barcodeBitmap = new Bitmap(imagePath);
            var barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(barcodeBitmap);
            Console.WriteLine($"Decoded Barcode: {result?.Text}");
            return result?.Text;
        }
    }
}
