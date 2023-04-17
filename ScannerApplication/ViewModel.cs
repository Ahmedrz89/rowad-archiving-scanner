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
        public ViewModel()
        {
            ApiService = new ApiService();
        }

        public ApiService ApiService { get; }
        private ObservableCollection<AttachmentTypeDto> _fileTypes;
        public ObservableCollection<AttachmentTypeDto> FileTypes => _fileTypes = _fileTypes ?? GetFileTypes();
        private ObservableCollection<AttachmentDto> _attachments;
        public ObservableCollection<AttachmentDto> Attachments => _attachments = _attachments ?? new ObservableCollection<AttachmentDto>();
        //public ObservableCollection<AttachmentDto> Attachments => _attachments = _attachments ?? GetTestData();
        

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

            if (ApiService == null)
                return new ObservableCollection<AttachmentTypeDto>();

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