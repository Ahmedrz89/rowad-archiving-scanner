using System.Windows.Media.Imaging;

namespace ScannerApplication.Models
{
    public class AttachmentDto
    {
        public AttachmentTypeDto FileType { get; set; }
        public string FilePath { get; set; }
        public BitmapImage Picture { get; set; }

    }
}