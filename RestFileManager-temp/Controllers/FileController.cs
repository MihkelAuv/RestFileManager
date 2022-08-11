using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFileManager_temp.Models;

namespace RestFileManager_temp.Controllers
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
            {
                metadata.id = Guid.NewGuid().ToString();
            }
            else
            {
                metadata = new Metadata
                {
                    id = Guid.NewGuid().ToString(),
                    description = null,
                    mimeType = file.ContentType,
                    name = file.FileName
                };
            }


            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(file.FileName, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return Ok(JsonSerializer.Serialize(metadata));
        }
    }
}
