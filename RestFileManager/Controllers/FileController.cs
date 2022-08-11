using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestFileManager.Models;

namespace RestFileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost(Name = "FileUpload")]
        public ActionResult Post([FromForm] MultipartFile filerequest)
        {
            Metadata? metadata = null;
            try
            {
                metadata = JsonSerializer.Deserialize<Metadata>(filerequest.MetaData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            var file = filerequest.File;
            if (metadata is not null)
                metadata.ID = Guid.NewGuid().ToString();
            else
                metadata = new Metadata(file.FileName, file.ContentType, string.Empty);
            
            if (file.Length > 0)
            {
                using var fileStream = new FileStream(file.FileName, FileMode.Create);
                file.CopyTo(fileStream);
            }

            return Ok(JsonSerializer.Serialize(metadata));
        }
    }
}
