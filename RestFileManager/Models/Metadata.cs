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

        public string ID { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }

        public string Description { get; set; }
    }
}
