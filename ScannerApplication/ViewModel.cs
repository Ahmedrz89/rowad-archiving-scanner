using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using ScannerApplication.Api;
using ScannerApplication.Models;

namespace ScannerApplication
{
    public class ViewModel
    {
        public ApiService ApiService { get; set; }
        private ObservableCollection<AttachmentTypeDto> _fileTypes;
        public ObservableCollection<AttachmentTypeDto> FileTypes => _fileTypes = _fileTypes ?? GetFileTypes();
        private ObservableCollection<AttachmentDto> _attachments;
        public ObservableCollection<AttachmentDto> Attachments => _attachments = _attachments ?? new ObservableCollection<AttachmentDto>();

        public ObservableCollection<AttachmentDto> GetTestData()
        {
            var result = new ObservableCollection<AttachmentDto>();
            for (int i = 0; i < 10; i++)
            {
                var item = new AttachmentDto
                {
                    FileType = FileTypes.FirstOrDefault(t => t.Id == 1),
                    FilePath = "F:\\Programs\\1.jpeg",
                };
                BitmapImage image = new BitmapImage(new Uri(item.FilePath));
                item.Picture = image;


                result.Add(item);
            }

            return result;
        }

        private ObservableCollection<AttachmentTypeDto> GetFileTypes()
        {

            //// Get the path to the application folder
            //string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;

            //// Construct the path to the file
            //string filePath = Path.Combine(appFolderPath, "FileTypes.json");
            //var jsonResult = JsonSerializer.Deserialize<List<JsonFileType>>(File.ReadAllText(filePath));
            //var result = new ObservableCollection<AttachmentTypeDto>();

            //if (jsonResult != null)
            //    foreach (var file in jsonResult)
            //    {
            //        result.Add(new AttachmentTypeDto()
            //        {
            //            Id = file.Id,
            //            DisplayName = file.ArabicName
            //        });
            //    }

            //return result;
            var docs = ApiService.GetDocumentTypes().GetAwaiter().GetResult();
            return new ObservableCollection<AttachmentTypeDto>(docs);
        }

        public void AddImages(List<BitmapImage> images)
        {
            foreach (var image in images)
            {
                _attachments.Add(new AttachmentDto
                {
                    Picture = image,
                    FileType = FileTypes.FirstOrDefault(t => t.Id == 1),
                });
            }
        }

  

     
    }
}