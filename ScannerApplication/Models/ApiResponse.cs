using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScannerApplication.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("error")]

        public ErrorResponse Error { get; set; }
    }

    public class ErrorResponse
    {
        [JsonPropertyName("code")]

        public int Code { get; set; }
        [JsonPropertyName("message")]

        public string Message { get; set; }
        [JsonPropertyName("details")]

        public string Details { get; set; }
        [JsonPropertyName("validationErrors")]

        public List<ValidationError> ValidationErrors { get; set; }

    }

    public class ValidationError
    {
        [JsonPropertyName("message")]

        public string Message { get; set; }
        [JsonPropertyName("members")]

        public List<string> Members { get; set; }
    }
}