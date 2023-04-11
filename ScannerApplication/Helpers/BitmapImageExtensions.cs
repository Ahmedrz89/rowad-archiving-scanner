using System.IO;
using System.Windows.Media.Imaging;

namespace ScannerApplication.Helpers
{
    public static class BitmapImageExtensions
    {
        public static byte[] ToByteArray(this BitmapImage bitmapImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}