namespace RestFileManager.Models
{
    public class MultipartFile
    {
        public string? MetaData { get; set; }
        public IFormFile File { get; set; }
    }
}
