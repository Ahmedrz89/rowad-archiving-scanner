using System;
using System.Collections.Generic;

namespace ScannerApplication.Models
{
    public class RealEstateAddAttachmentInput
    {
        public RealEstateAddAttachmentInput()
        {
            Attachments = new List<AttachmentInput>();
        }
        public Guid RealEstateId { get; set; }
        public List<AttachmentInput> Attachments { get; set; }

    }

    
    public class AttachmentInput
    {
        public Guid Id { get; set; }
        public int AttachmentType { get; set; }
    }
}