using System.Text.Json.Serialization;

namespace ScannerApplication.Models
{
    public class JsonFileType
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("name_ar")]
        public string ArabicName { get; set; }
    }
}