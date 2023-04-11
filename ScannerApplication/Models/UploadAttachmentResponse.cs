using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScannerApplication.Models
{
    public class UploadAttachmentResponse : ApiResponse
    {
        [JsonPropertyName("result")]
        public List<Guid> Result { get; set; }
    }
}