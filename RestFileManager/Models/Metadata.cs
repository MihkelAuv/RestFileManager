using System;
using System.Text.Json.Serialization;

namespace RestFileManager.Models
{
    public class Metadata
    {
        public Metadata(string name, string mimeType, string description)
        {
            ID = Guid.NewGuid().ToString();
            Name = name;
            MimeType = mimeType;
            Description = description;
        }

        [JsonPropertyName("id")]
        public string? ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
