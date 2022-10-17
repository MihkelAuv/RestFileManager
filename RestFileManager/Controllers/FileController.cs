using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFileManager.Models;
using RestFileManager.Services;

namespace RestFileManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("Upload1")]
        public ActionResult Upload1([FromForm] MultipartFile filerequest)
        {
            Metadata? metadata = null;
            try
            {
                metadata = JsonSerializer.Deserialize<Metadata>(filerequest.MetaData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }

            var file = filerequest.File;
            if (metadata is not null)
                metadata.ID = Guid.NewGuid().ToString();
            else
                metadata = new Metadata(file.FileName, file.ContentType, string.Empty);
            try
            {
                FileService.SaveFile(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }
            
            return Ok(JsonSerializer.Serialize(metadata));
        }

        [HttpPost("Upload2")]
        public IActionResult Upload2([ModelBinder(BinderType = typeof(MetadataModelProvider))] Metadata metadata, [Required] IFormFile file)
        {
            metadata.ID = Guid.NewGuid().ToString();

            try
            {
                FileService.SaveFile(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.InnerException?.ToString());
            }

            return Ok(JsonSerializer.Serialize(metadata));
        }
    }
}
